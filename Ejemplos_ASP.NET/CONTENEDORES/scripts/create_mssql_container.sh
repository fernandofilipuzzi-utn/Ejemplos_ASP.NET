#!/bin/bash

TAG='v0.1'

NOMBRE_IMAGEN='mssql_database_image'
NOMBRE_CONTENEDOR='mssql_database_container'
DOCKER_FILE='/workspaces/Ejemplos_ASP.NET/Ejemplos_ASP.NET/CONTENEDORES/dockerfiles/Dockerfile.mssql'

SOLUCION_PATH='/'

# paro el contenedor - por si esta corriendo
#docker stop $NOMBRE_CONTENEDOR
# borro el contenedor por si ya estaba
# docker rm $NOMBRE_CONTENEDOR
docker stop $NOMBRE_CONTENEDOR 2>/dev/null && docker rm $NOMBRE_CONTENEDOR 2>/dev/null


# borro la imagen
if docker images | grep -q "$NOMBRE_IMAGEN.*$TAG"; then
  docker rmi $NOMBRE_IMAGEN:$TAG
fi

# construyo la imagen
docker build  --no-cache -f $DOCKER_FILE -t $NOMBRE_IMAGEN:$TAG $SOLUCION_PATH

# genero el contenedor y lo corro
docker run --name $NOMBRE_CONTENEDOR -p 1433:1433 -d $NOMBRE_IMAGEN:$TAG

# listo los contenedores corriendo
docker ps 

# observo el status del contenedor
docker logs $NOMBRE_CONTENEDOR

# espero hasta levante el contenedor
sleep 20

docker exec -it $NOMBRE_CONTENEDOR /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'MSS-fernando-123' -i /src/sql_script/docker_script.sql -C

# Conexion desde el host
#docker exec -it ejemplo05_mssql_container /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'MSS-fernando-123' -i /src/sql_script/docker_script.sql -C
#docker exec -it ejemplo05_mssql_container /bin/bash

# consulta de la ip
docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}'  $NOMBRE_CONTENEDOR

#IP_CONTENEDOR=docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' $NOMBRE_CONTENEDOR
# configurando la base de datos
#docker exec -it $NOMBRE_CONTENEDOR /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'MSS-fernando-123' -i /src/sql_script/docker_script.sql
#/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'MSS-fernando-123' -i /src/sql_script/docker_script.sql -C

