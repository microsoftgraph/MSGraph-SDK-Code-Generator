#!/bin/sh

#
# Utility alias for building vipr-t4templatewriter in POSIX platforms with mono.
#

xbuild /property:Configuration=Debug /property:Platform="Any CPU" /target:Rebuild vipr-t4templatewriter.sln

