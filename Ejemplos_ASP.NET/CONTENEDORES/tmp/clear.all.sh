#!/bin/bash

# parar todo los contenedores que comienzan con ejemplo
docker ps -q --filter "name=^ejemplo" | xargs docker stop

# borrar todas las imágenes
docker rmi $(docker images -q) -f

# elimina todas las capas de la cache
docker builder prune --all

# eliminar los contenedios detenidos y volumens y datos en cache
docker system prune --all --volumes -f

# limpia todo con un solo comando
# imagenes, contenedores detenidos, cache y volúmenes
docker system prune --all --volumes -f