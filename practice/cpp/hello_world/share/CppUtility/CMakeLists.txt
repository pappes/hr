########################################################################################################################################################
###
### Copyright (C) 2018 by pappes
###
### This file is part of the hw project.
###
########################################################################################################################################################

### @file                       CppProject/CppUtility/CMakeLists.txt
### @author                     pappes <pappes@gmail.com>
### @date                       9 Jun 2018

########################################################################################################################################################

## Project Properties

SET (PROJECT_NAME "hw")
SET (PROJECT_DESCRIPTION "compiler test")
SET (PROJECT_PATH "CppProject")
SET (PROJECT_PACKAGE_NAME "hw-test")
SET (PROJECT_LICENSE "MIT License")
SET (PROJECT_VENDOR_ID "hack.pappes")
SET (PROJECT_VENDOR_NAME "N/A")
SET (PROJECT_VENDOR_CONTACT "pappes@gmail.com")
SET (PROJECT_VENDOR_URL "www.company.com")

########################################################################################################################################################

## Setup

### Compatibility Check

CMAKE_MINIMUM_REQUIRED (VERSION "2.8.12" FATAL_ERROR)

### Paths

SET (CMAKE_MODULE_PATH "${CMAKE_CURRENT_SOURCE_DIR}/tools/cmake")

### Policies

CMAKE_POLICY (SET "CMP0048" NEW)

########################################################################################################################################################

## Project Configuration

PROJECT (${PROJECT_NAME} VERSION ${PROJECT_VERSION_STRING} LANGUAGES "C" "CXX")

########################################################################################################################################################

## Targets

SET (EXECUTABLE_NAME "${PROJECT_PACKAGE_NAME}.exe")
SET (EXECUTABLE_TARGET ${EXECUTABLE_NAME})

FILE (GLOB_RECURSE EXE_HEADERS "${PROJECT_SOURCE_DIR}/include/*.hpp")
FILE (GLOB_RECURSE EXE_SRCS "${PROJECT_SOURCE_DIR}/src/*.cpp" "${PROJECT_SOURCE_DIR}/src/*.cxx")

ADD_EXECUTABLE (${EXECUTABLE_TARGET} ${EXE_SRCS})

ADD_DEPENDENCIES (${EXECUTABLE_TARGET} ${SHARED_LIBRARY_TARGET})

TARGET_INCLUDE_DIRECTORIES (${EXECUTABLE_TARGET} PUBLIC "${CMAKE_SOURCE_DIR}/include")
TARGET_INCLUDE_DIRECTORIES (${EXECUTABLE_TARGET} PUBLIC "${PROJECT_SOURCE_DIR}/include")

TARGET_LINK_LIBRARIES (${EXECUTABLE_TARGET} "pthread")
TARGET_LINK_LIBRARIES (${EXECUTABLE_TARGET} ${SHARED_LIBRARY_TARGET})

SET_TARGET_PROPERTIES (${EXECUTABLE_TARGET} PROPERTIES VERSION ${PROJECT_VERSION_STRING} OUTPUT_NAME ${EXECUTABLE_NAME} CLEAN_DIRECT_OUTPUT 1)

INSTALL (TARGETS ${EXECUTABLE_TARGET} DESTINATION ${INSTALL_BIN} COMPONENT "applications")

########################################################################################################################################################