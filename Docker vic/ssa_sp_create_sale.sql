--user type, to pass a long list of values, 
--in this case, list of products from API to DB.
CREATE TYPE ListProductsToBuy AS TABLE(
     /*  id_SolicitudConstruccion INT NOT NULL, */ /* El SP le da el valor */
      idProduct INT ,
      amount INT
    );

CREATE PROCEDURE ssa_sp_create_sale
	-- Add the parameters for the stored procedure here
	@regionId int = 1,
	--products
    @productsIdList ListProductsToBuy READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN TRY

		DECLARE @tranName NVARCHAR(50) = 'Create_Sale';

        BEGIN TRANSACTION @tranName;

            INSERT INTO dbo.Sale (taxesRegionId)
            VALUES (@regionId);

            DECLARE @saleId INT = SCOPE_IDENTITY();
            --create record on Sale_Product

            --Check all products exists or throw error

            IF(
                (
                    SELECT COUNT(P.Id) 
                    FROM @productsIdList pil
                        INNER JOIN dbo.Product P
                        ON P.Id = pil.idProduct
                )
                !=
                (
                    SELECT COUNT(pil.idProduct) 
                    FROM @productsIdList pil
                )

            )
            BEGIN 
                THROW 54321, 'Algunos de los productos requeridos no existen.',1;            
            END


            --Check enough inventory
           /*  ;WITH InvCalc (ProdId, AmntReqrd, InvDiff)
            AS
                (
                    SELECT 
                        pil.idProduct, pil.amount AS AmntReqrd, 
                        (P.inventoryAmount - pil.amount) AS InvDiff
                    FROM @productsIdList pil
                        INNER JOIN dbo.Product P
                        ON P.Id = pil.idProduct
                )  
                --Must be followed by SELECT inSERT UPDATE OR DELETE
            IF EXISTS(SELECT InvDiff FROM InvCalc WHERE InvDiff < 0)
            BEGIN
                THROW 54321, 'Algunos de los productos requeridos no están en existencia.',1;
            END
 */

            DECLARE @Temp TABLE (InvDiff integer);


            ;WITH InvCalc (ProdId, AmntReqrd, InvDiff)
            AS
                (
                    SELECT 
                        pil.idProduct, pil.amount AS AmntReqrd, 
                        (P.inventoryAmount - pil.amount) AS InvDiff
                    FROM @productsIdList pil
                        INNER JOIN dbo.Product P
                        ON P.Id = pil.idProduct
                )  
                --Must be followed by SELECT inSERT UPDATE OR DELETE
            /* IF EXISTS(SELECT InvDiff FROM InvCalc WHERE InvDiff < 0)
            BEGIN
                THROW 54321, 'Algunos de los productos requeridos no están en existencia.',1;
            END */
            INSERT INTO @Temp
            SELECT InvDiff FROM InvCalc;

            IF EXISTS(SELECT InvDiff FROM @Temp WHERE InvDiff < 0)
                BEGIN
                    THROW 54321, 'Algunos de los productos requeridos no están en existencia.',1;
                END 

            --Checks passed

            INSERT INTO dbo.Sale_Product (idSale, idProduct, amount)
            SELECT @saleId, UsrProds.idProduct, UsrProds.amount FROM @productsIdList as UsrProds
            
            UPDATE dbo.Product 
            SET dbo.Product.inventoryAmount =  (P.inventoryAmount - pil.amount)
            FROM @productsIdList pil
                INNER JOIN dbo.Product P
                ON P.Id = pil.idProduct

            --Update each product inventory



        COMMIT TRANSACTION @tranName;

    END TRY
    BEGIN CATCH

        ROLLBACK TRANSACTION @tranName;
        throw;
        /* SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_STATE() AS ErrorState,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage; */


    END CATCH

    --Create the entry sale

END
GO

