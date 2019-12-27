#!/bin/bash
set -e
echo "Branch - ${TRAVIS_BRANCH}"
if [[ ("${TRAVIS_PULL_REQUEST}" = "false") && ("${TRAVIS_BRANCH}" =~ ^(master|dev|qa|model)$) ]]; then
    echo "Packaging artifacts"
    docker build -t marsImages:${PACKAGE_VERSION_NUMBER} . --build-arg build_version=${BUILD_VERSION_NUMBER}
fi