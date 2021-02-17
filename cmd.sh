#!/usr/bin/env bash


#[[-------------------------------------------------------------------- Settings
set -o errexit   # abort on nonzero exitstatus
set -o nounset   # abort on unbound variable
set -o pipefail  # don't hide errors within pipes
# set -o xtrace
#]]


#[[-------------------------------------------------------------------- Utils
usage(){
    printf "%s OPTION\n\n" "${0}"
}

help_options(){
  printf "Options :\n"
  printf "  General options :\t\n"
  printf "    -h, --help         	Print this help text and exit\n"
  printf "    -b, --bash         	Run bash command in order to connect the container\n"
  printf "    -br, --build-run		Build then run then run the container, should use docker-compose instead\n"
}

usage(){
  printf "$0"
}


if [ $# -eq 2 ]; then
  printf ""

cmd=podman
#cmd=docker <---- if using docker

## Build the image from the dockerfile 
$cmd build -t turradgiver -f turradgiver-api/Dockerfile .

## build the container, could, be intressted to add volume
$cmd run -d -p 3000:5000 -v ./turradgiver-api/:/src/turradgiver-api ./data-access/:/src/data-access turradgiver

