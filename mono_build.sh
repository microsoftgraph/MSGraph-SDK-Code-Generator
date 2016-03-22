#!/bin/sh

#
# Utility alias for building the solution in POSIX platforms with mono.
# Note: Run at the same directory where it is located.
#


xbuild /property:Configuration=Debug /property:Platform="Any CPU" /target:Rebuild GraphODataTemplateWriter.sln
