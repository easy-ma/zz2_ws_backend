#!/usr/bin/env bash

cmd=podman
#cmd=docker <---- if using docker

## Build the image from the dockerfile 
$cmd build -t turradgiver -f turradgiver-api/Dockerfile .

## build the container, could, be intressted to add volume
$cmd run -d -p 3000:5000 -v ./turradgiver-api/:/src/turradgiver-api ./data-access/:/src/data-access turradgiver

