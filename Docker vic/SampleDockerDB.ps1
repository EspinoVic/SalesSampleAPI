##docker pull mcr.microsoft.com/mssql/server
docker pull mcr.microsoft.com/mssql/server:2019-latest
#https://hub.docker.com/_/microsoft-mssql-server
if( (docker ps -a  -f name="HerosSample").count -eq 1){ # 1 = no exists
    Write-host "NO"
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Benito@31" -p 1433:1433 -d --name "HerosSample" mcr.microsoft.com/mssql/server:2019-latest
} 
else{
    Write-host "SI"
}
#docker image inspect "mcr.microsoft.com/mssql/server"
 
