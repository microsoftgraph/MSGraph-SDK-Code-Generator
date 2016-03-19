#!/bin/sh

#
# Utility alias for building the solution in POSIX platforms with mono.
#

xbuild /property:Configuration=Debug /property:Platform="Any CPU" /target:Rebuild GraphODataTemplateWriter.sln

