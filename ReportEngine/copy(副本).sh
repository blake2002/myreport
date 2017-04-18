#!/bin/bash
echo $1 $2
val1=$2
val2=${val1}"/\$(Configuration)"
echo $val2
cp $1 $val2