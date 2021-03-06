########################################################################################################################################################
###
### Copyright (C) 2018 by pappes
###
### This file is part of the hw project.
###
########################################################################################################################################################

### @file                       CppProject/CMakeLists.txt
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
SET (PROJECT_VENDOR_NAME "pappes")
SET (PROJECT_VENDOR_CONTACT "pappes@gmail.com")
SET (PROJECT_VENDOR_URL "www.company.com")

########################################################################################################################################################

## Project Options

OPTION (BUILD_SHARED_LIBRARY "Build shared library." ON)
OPTION (BUILD_STATIC_LIBRARY "Build static library." OFF)
OPTION (BUILD_UTILITY "Build utility." ON)
OPTION (BUILD_UNIT_TESTS "Build tests" ON)
OPTION (BUILD_CODE_COVERAGE "Build code coverage" ON)
OPTION (BUILD_DOCUMENTATION "Build documentation" ON)
OPTION (INSTALL_DATA "Install data" ON)
# TODO DH Hack SET (BUILD_SHARED_LIBRARY "ON") #because the option above results in the varioable being OFF
SET (BUILD_UNIT_TESTS "OFF") #because the option above results in the varioable being ON
SET (BUILD_CODE_COVERAGE "OFF") #because the option above results in the varioable being ON
SET (BUILD_DOCUMENTATION "OFF") #because the option above results in the varioable being ON

########################################################################################################################################################

## Setup

### Compatibility Check

CMAKE_MINIMUM_REQUIRED (VERSION "2.8.12" FATAL_ERROR)

### Paths

SET (CMAKE_MODULE_PATH "${CMAKE_CURRENT_SOURCE_DIR}/tools/cmake")

### Policies

CMAKE_POLICY (SET "CMP0048" NEW)

########################################################################################################################################################

## Version

INCLUDE ("GetGitRevisionDescription" OPTIONAL)

IF (EXISTS "${PROJECT_SOURCE_DIR}/.git/packed-refs" AND COMMAND "GET_GIT_HEAD_REVISION" AND COMMAND "GIT_DESCRIBE")

    # Versioning using current git branch tag

    GET_GIT_HEAD_REVISION (GIT_REFSPEC GIT_HASH)
    GIT_DESCRIBE (GIT_TAGS "--tags")

    STRING (REPLACE "." ";" VERSION_LIST ${GIT_TAGS})

    IF (VERSION_LIST)

        LIST (GET VERSION_LIST 0 PROJECT_VERSION_MAJOR)
        LIST (GET VERSION_LIST 1 PROJECT_VERSION_MINOR)
        LIST (GET VERSION_LIST 2 PROJECT_VERSION_PATCH_TWEAK)

        STRING (REPLACE "-" ";" PROJECT_VERSION_PATCH_TWEAKS ${PROJECT_VERSION_PATCH_TWEAK})

        LIST (GET PROJECT_VERSION_PATCH_TWEAKS 0 PROJECT_VERSION_PATCH)

        LIST (LENGTH PROJECT_VERSION_PATCH_TWEAKS PROJECT_VERSION_PATCH_TWEAKS_LENGTH)

        IF (PROJECT_VERSION_PATCH_TWEAKS_LENGTH GREATER 1)

            LIST (GET PROJECT_VERSION_PATCH_TWEAKS 1 PROJECT_VERSION_TWEAK)

        ENDIF ()        

        SET (PROJECT_VERSION_STRING "${PROJECT_VERSION_MAJOR}.${PROJECT_VERSION_MINOR}.${PROJECT_VERSION_PATCH}")

        IF (DEFINED "PROJECT_VERSION_TWEAK")

            SET (PROJECT_VERSION_STRING "${PROJECT_VERSION_STRING}.${PROJECT_VERSION_TWEAK}")
       
        ENDIF ()

    ENDIF ()

ENDIF ()

IF (NOT DEFINED PROJECT_VERSION_STRING)

    # Versioning using current date / time

    STRING (TIMESTAMP CURRENT_YEAR %Y UTC)
    STRING (TIMESTAMP CURRENT_MONTH %m UTC)
    STRING (TIMESTAMP CURRENT_DAY %d UTC)
    STRING (TIMESTAMP CURRENT_DAY_OF_YEAR %j UTC)

    SET (PROJECT_VERSION_MAJOR ${CURRENT_YEAR})
    SET (PROJECT_VERSION_MINOR ${CURRENT_MONTH})
    SET (PROJECT_VERSION_PATCH ${CURRENT_DAY})

    SET (PROJECT_VERSION_STRING "${PROJECT_VERSION_MAJOR}.${PROJECT_VERSION_MINOR}.${PROJECT_VERSION_PATCH}")

ENDIF ()

MESSAGE (STATUS "Version: ${PROJECT_VERSION_STRING}")

########################################################################################################################################################

## Project Configuration

PROJECT (${PROJECT_NAME} VERSION ${PROJECT_VERSION_STRING} LANGUAGES "C" "CXX")

SET (CMAKE_CONFIGURATION_TYPES "Debug;Release" CACHE STRING "Limited Configs" FORCE)

IF (NOT CMAKE_BUILD_TYPE)
    SET (CMAKE_BUILD_TYPE Release CACHE STRING "Build type [None|Debug|Release|RelWithDebInfo|MinSizeRel]." FORCE)
ENDIF ()

########################################################################################################################################################

## Flags

### Warnings

IF (CMAKE_CXX_COMPILER_ID STREQUAL "GNU")

    IF (CMAKE_CXX_COMPILER_VERSION VERSION_LESS 4.8)

        MESSAGE (FATAL_ERROR "GCC version must be at least 4.8")
    
    ENDIF ()

    SET (CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -Wextra -Wpedantic -Wshadow -Wno-deprecated -Wl,--no-undefined")

ENDIF ()

### C++ 14 support

SET (CMAKE_CXX_STANDARD 14)
SET (CMAKE_CXX_STANDARD_REQUIRED ON)
SET (CMAKE_CXX_EXTENSIONS OFF)

########################################################################################################################################################

## Debugging Options

SET (CMAKE_VERBOSE_MAKEFILE 0) # Use 1 for debugging, 0 for release

########################################################################################################################################################

## Paths

### Search Paths

LIST (APPEND CMAKE_PREFIX_PATH "${PROJECT_SOURCE_DIR}")
LIST (APPEND CMAKE_PREFIX_PATH "${PROJECT_SOURCE_DIR}/tools")
LIST (APPEND CMAKE_PREFIX_PATH "${PROJECT_SOURCE_DIR}/thirdparty")

### Output Paths

SET (EXECUTABLE_OUTPUT_PATH "${PROJECT_SOURCE_DIR}/bin")
SET (LIBRARY_OUTPUT_PATH "${PROJECT_SOURCE_DIR}/lib")
SET (DOCS_OUTPUT_PATH "${PROJECT_SOURCE_DIR}/docs")

IF (WIN32)

    SET (INSTALL_ROOT ".")                              # C:\Programme\<project>
    SET (INSTALL_DATA "bin")                            # C:\Programme\<project>
    SET (INSTALL_BIN "bin")                             # C:\Programme\<project>
    SET (INSTALL_LIB "lib")                             # C:\Programme\<project>\lib
    SET (INSTALL_INCLUDE "include")                     # C:\Programme\<project>\include
    SET (INSTALL_DOC "doc")                             # C:\Programme\<project>\doc
    SET (INSTALL_SHORTCUTS ".")                         # Not available under Windows
    SET (INSTALL_ICONS ".")                             # Not available under Windows
    SET (INSTALL_INIT ".")                              # Not available under Windows

ELSE ()

    SET (INSTALL_ROOT "share/${PROJECT_PATH}")          # /usr/[local]/share/<project>
    SET (INSTALL_DATA "share/${PROJECT_PATH}")          # /usr/[local]/share/<project>
    SET (INSTALL_BIN "bin")                             # /usr/[local]/bin
    SET (INSTALL_LIB "lib")                             # /usr/[local]/lib
    SET (INSTALL_TEST "test/${PROJECT_PATH}")           # /usr/[local]/test/<project>
    SET (INSTALL_INCLUDE "include/${PROJECT_PATH}")     # /usr/[local]/include/<project>
    SET (INSTALL_DOC "share/doc/${PROJECT_PATH}")       # /usr/[local]/share/doc/<project>
    SET (INSTALL_SHORTCUTS "share/applications")        # /usr/[local]/share/applications
    SET (INSTALL_INIT "/etc/init")                      # /etc/init (upstart init scripts)

ENDIF ()

########################################################################################################################################################

## Configure Files

FILE (GLOB_RECURSE CONFIGINPUTS1 "include/*.in.hpp.cmake")
FILE (GLOB_RECURSE CONFIGINPUTS2 "include/*.hpp.in.cmake")
FILE (GLOB_RECURSE CONFIGINPUTS3 "src/*.in.hpp.cmake")
FILE (GLOB_RECURSE CONFIGINPUTS4 "src/*.hpp.in.cmake")
FILE (GLOB_RECURSE CONFIGINPUTS5 "test/*.in.hpp.cmake")
FILE (GLOB_RECURSE CONFIGINPUTS6 "test/*.hpp.in.cmake")

LIST (APPEND CONFIGINPUTS "${CONFIGINPUTS1}")
LIST (APPEND CONFIGINPUTS "${CONFIGINPUTS2}")
LIST (APPEND CONFIGINPUTS "${CONFIGINPUTS3}")
LIST (APPEND CONFIGINPUTS "${CONFIGINPUTS4}")
LIST (APPEND CONFIGINPUTS "${CONFIGINPUTS5}")
LIST (APPEND CONFIGINPUTS "${CONFIGINPUTS6}")

FOREACH (CONFIGINPUT ${CONFIGINPUTS})

    STRING (REPLACE ".in.hpp.cmake" ".h" CONFIGOUTPUT1 "${CONFIGINPUT}")
    STRING (REPLACE ".hpp.in.cmake" ".h" CONFIGOUTPUT2 "${CONFIGOUTPUT1}")
    
    CONFIGURE_FILE ("${CONFIGINPUT}" "${CONFIGOUTPUT2}")

ENDFOREACH ()

########################################################################################################################################################

## Dependencies

# FIND_PACKAGE (OpenMP)

# IF (OPENMP_FOUND)
#     SET (CMAKE_C_FLAGS "${CMAKE_C_FLAGS} ${OpenMP_C_FLAGS}")
#     SET (CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${OpenMP_CXX_FLAGS}")
# ELSE ()
#     MESSAGE (SEND_ERROR "[OpenMP] not found.")
# ENDIF ()

########################################################################################################################################################

## Versioning

IF (DEFINED PROJECT_VERSION_STRING AND EXISTS "${PROJECT_SOURCE_DIR}/src/${PROJECT_PATH}/Utilities/Version.cpp.in")

    CONFIGURE_FILE ("${PROJECT_SOURCE_DIR}/src/${PROJECT_PATH}/Utilities/Version.cpp.in" "${CMAKE_CURRENT_BINARY_DIR}/src/${PROJECT_PATH}/Utilities/Version.cpp" @ONLY)

ENDIF ()

########################################################################################################################################################

## Targets

### Shared Library

IF (BUILD_SHARED_LIBRARY)

    SET (SHARED_LIBRARY_NAME "${PROJECT_PACKAGE_NAME}")
    SET (SHARED_LIBRARY_TARGET "lib${SHARED_LIBRARY_NAME}.so")
    #MESSAGE (STATUS "Real SHARED_LIBRARY_TARGET: ${SHARED_LIBRARY_TARGET}")

    FILE (GLOB_RECURSE SHARED_LIBRARY_HEADERS "${PROJECT_SOURCE_DIR}/include/${PROJECT_PATH}/*.hpp")
    FILE (GLOB_RECURSE SHARED_LIBRARY_SRCS "${PROJECT_SOURCE_DIR}/src/${PROJECT_PATH}/*.cpp")

    ADD_LIBRARY (${SHARED_LIBRARY_TARGET} SHARED ${SHARED_LIBRARY_SRCS})

    TARGET_INCLUDE_DIRECTORIES (${SHARED_LIBRARY_TARGET} PUBLIC "${PROJECT_SOURCE_DIR}/include/")

    TARGET_LINK_LIBRARIES (${SHARED_LIBRARY_TARGET} "pthread")

    SET_TARGET_PROPERTIES (${SHARED_LIBRARY_TARGET} PROPERTIES VERSION ${PROJECT_VERSION_STRING} SOVERSION ${PROJECT_VERSION_MAJOR} OUTPUT_NAME ${SHARED_LIBRARY_NAME} CLEAN_DIRECT_OUTPUT 1 INSTALL_RPATH "$ORIGIN/../lib:$ORIGIN/")

    INSTALL (DIRECTORY "${PROJECT_SOURCE_DIR}/include/${PROJECT_PATH}/" DESTINATION ${INSTALL_INCLUDE} COMPONENT "headers" FILES_MATCHING PATTERN "*.hpp")
    INSTALL (DIRECTORY "${PROJECT_SOURCE_DIR}/src/${PROJECT_PATH}/" DESTINATION ${INSTALL_INCLUDE} COMPONENT "headers" FILES_MATCHING PATTERN "*.tpp")
    INSTALL (TARGETS ${SHARED_LIBRARY_TARGET} DESTINATION ${INSTALL_LIB} COMPONENT "libraries")

ENDIF ()

### Static Library

IF (BUILD_STATIC_LIBRARY)

    SET (STATIC_LIBRARY_NAME "${PROJECT_PACKAGE_NAME}")
    SET (STATIC_LIBRARY_TARGET "lib${STATIC_LIBRARY_NAME}.a")

    FILE (GLOB_RECURSE STATIC_LIBRARY_HEADERS "${PROJECT_SOURCE_DIR}/include/${PROJECT_PATH}/*.hpp")
    FILE (GLOB_RECURSE STATIC_LIBRARY_SRCS "${PROJECT_SOURCE_DIR}/src/${PROJECT_PATH}/*.cpp")

    ADD_LIBRARY (${STATIC_LIBRARY_TARGET} STATIC ${STATIC_LIBRARY_SRCS})

    TARGET_INCLUDE_DIRECTORIES (${STATIC_LIBRARY_TARGET} PUBLIC "${PROJECT_SOURCE_DIR}/include/")

    TARGET_LINK_LIBRARIES (${STATIC_LIBRARY_TARGET} "pthread")

    SET_TARGET_PROPERTIES (${STATIC_LIBRARY_TARGET} PROPERTIES VERSION ${PROJECT_VERSION_STRING} OUTPUT_NAME ${SHARED_LIBRARY_NAME} CLEAN_DIRECT_OUTPUT 1 INSTALL_RPATH "$ORIGIN/../lib:$ORIGIN/")

    INSTALL (DIRECTORY "${PROJECT_SOURCE_DIR}/include/${PROJECT_PATH}/" DESTINATION ${INSTALL_INCLUDE} COMPONENT "headers" FILES_MATCHING PATTERN "*.hpp")
    INSTALL (DIRECTORY "${PROJECT_SOURCE_DIR}/src/${PROJECT_PATH}/" DESTINATION ${INSTALL_INCLUDE} COMPONENT "headers" FILES_MATCHING PATTERN "*.tpp")
    INSTALL (TARGETS ${SHARED_LIBRARY_TARGET} DESTINATION ${INSTALL_LIB} COMPONENT "libraries")

ENDIF ()

### Utility 

IF (BUILD_UTILITY)

    ADD_SUBDIRECTORY ("${PROJECT_SOURCE_DIR}/share/CppUtility")

ENDIF ()

### Unit Tests

IF (BUILD_UNIT_TESTS)

    SET (UNIT_TESTS_TARGET "${PROJECT_PACKAGE_NAME}.test")

    FILE (GLOB_RECURSE TEST_SRCS "${PROJECT_SOURCE_DIR}/test/${PROJECT_PATH}/*.test.cpp")

    ADD_EXECUTABLE (${UNIT_TESTS_TARGET} ${TEST_SRCS})

    FIND_PACKAGE ("GTest")

    IF (NOT GTEST_FOUND)
            
        ADD_SUBDIRECTORY ("${PROJECT_SOURCE_DIR}/thirdparty/gtest")

        ADD_DEPENDENCIES (${UNIT_TESTS_TARGET} "googletest")

        LIST (APPEND GTEST_BOTH_LIBRARIES "${GTEST_LIBS_DIR}/libgtest.a")
        LIST (APPEND GTEST_BOTH_LIBRARIES "${GTEST_LIBS_DIR}/libgtest_main.a")
        
    ENDIF ()

    ENABLE_TESTING ()

    ADD_DEPENDENCIES (${UNIT_TESTS_TARGET} ${SHARED_LIBRARY_TARGET})

    TARGET_INCLUDE_DIRECTORIES (${UNIT_TESTS_TARGET} PUBLIC ${GTEST_INCLUDE_DIRS})
    TARGET_INCLUDE_DIRECTORIES (${UNIT_TESTS_TARGET} PUBLIC "${PROJECT_SOURCE_DIR}/include")
    TARGET_INCLUDE_DIRECTORIES (${UNIT_TESTS_TARGET} PUBLIC "${PROJECT_SOURCE_DIR}/test")

    TARGET_LINK_LIBRARIES (${UNIT_TESTS_TARGET} "pthread")
    TARGET_LINK_LIBRARIES (${UNIT_TESTS_TARGET} ${GTEST_BOTH_LIBRARIES})
    TARGET_LINK_LIBRARIES (${UNIT_TESTS_TARGET} "${SHARED_LIBRARY_TARGET}")

    SET_TARGET_PROPERTIES (${UNIT_TESTS_TARGET} PROPERTIES VERSION ${PROJECT_VERSION_STRING} OUTPUT_NAME ${UNIT_TESTS_TARGET} CLEAN_DIRECT_OUTPUT 1 INSTALL_RPATH "$ORIGIN/../lib:$ORIGIN/")

    ADD_TEST (NAME "all" COMMAND ${UNIT_TESTS_TARGET} "${CMAKE_CURRENT_LIST_DIR}/test")

    INSTALL (TARGETS ${UNIT_TESTS_TARGET} DESTINATION ${INSTALL_TEST} COMPONENT "tests")
    
    ADD_CUSTOM_TARGET ("check" "${PROJECT_SOURCE_DIR}/bin/${UNIT_TESTS_TARGET}" "${CMAKE_CURRENT_LIST_DIR}/test" DEPENDS ${UNIT_TESTS_TARGET} COMMENT "Executing unit tests..." VERBATIM SOURCES ${TEST_SRCS})

    #### Code Coverage

    IF (BUILD_CODE_COVERAGE)

        SET (COVERAGE_TARGET "coverage")

        INCLUDE (CodeCoverage)

        APPEND_COVERAGE_COMPILER_FLAGS ()

        SET (COVERAGE_EXCLUDES  "${CMAKE_CURRENT_BINARY_DIR}/thirdparty/gtest/src/googletest/googletest/include/gtest/*"
                                "${CMAKE_CURRENT_BINARY_DIR}/thirdparty/gtest/src/googletest/googletest/include/gtest/internal/*")

        SETUP_TARGET_FOR_COVERAGE (
            NAME ${COVERAGE_TARGET}
            EXECUTABLE ${UNIT_TESTS_TARGET}
            DEPENDENCIES "coverage"
        )

    ENDIF ()

ENDIF ()

### Documentation

IF (BUILD_DOCUMENTATION)

    SET (DOCUMENTATION_TARGET "docs")

    SET (DOXYFILE_IN "${PROJECT_SOURCE_DIR}/tools/doxygen/Doxyfile.in")
    SET (DOXYFILE "${CMAKE_CURRENT_BINARY_DIR}/Doxyfile")

    CONFIGURE_FILE (${DOXYFILE_IN} ${DOXYFILE} @ONLY)

    CONFIGURE_FILE ("${PROJECT_SOURCE_DIR}/tools/doxygen/doxygen-bootstrapped/example-site/header.html" "${CMAKE_CURRENT_BINARY_DIR}/header.html" COPYONLY)
    CONFIGURE_FILE ("${PROJECT_SOURCE_DIR}/tools/doxygen/doxygen-bootstrapped/example-site/footer.html" "${CMAKE_CURRENT_BINARY_DIR}/footer.html" COPYONLY)
    CONFIGURE_FILE ("${PROJECT_SOURCE_DIR}/tools/doxygen/doxygen-bootstrapped/customdoxygen.css" "${CMAKE_CURRENT_BINARY_DIR}/customdoxygen.css" COPYONLY)
    CONFIGURE_FILE ("${PROJECT_SOURCE_DIR}/tools/doxygen/doxygen-bootstrapped/doxy-boot.js" "${CMAKE_CURRENT_BINARY_DIR}/doxy-boot.js" COPYONLY)
    CONFIGURE_FILE ("${PROJECT_SOURCE_DIR}/tools/doxygen/doxygen-bootstrapped/jquery.smartmenus.js" "${CMAKE_CURRENT_BINARY_DIR}/jquery.smartmenus.js" COPYONLY)
    CONFIGURE_FILE ("${PROJECT_SOURCE_DIR}/tools/doxygen/doxygen-bootstrapped/addons/bootstrap/jquery.smartmenus.bootstrap.js" "${CMAKE_CURRENT_BINARY_DIR}/jquery.smartmenus.bootstrap.js" COPYONLY)
    CONFIGURE_FILE ("${PROJECT_SOURCE_DIR}/tools/doxygen/doxygen-bootstrapped/addons/bootstrap/jquery.smartmenus.bootstrap.css" "${CMAKE_CURRENT_BINARY_DIR}/jquery.smartmenus.bootstrap.css" COPYONLY)

    FIND_PACKAGE ("Doxygen")
    
    IF (NOT DOXYGEN_FOUND)
            
        ADD_SUBDIRECTORY ("${PROJECT_SOURCE_DIR}/thirdparty/doxygen")

        SET (DOXYGEN_EXECUTABLE "${CMAKE_CURRENT_BINARY_DIR}/thirdparty/doxygen/source/bin/doxygen")

        ADD_CUSTOM_TARGET (${DOCUMENTATION_TARGET} ALL
            COMMAND ${DOXYGEN_EXECUTABLE} ${DOXYFILE}
            WORKING_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}
            DEPENDS "doxygen"
            COMMENT "Generating API documentation with Doxygen"
            VERBATIM)

    ELSE ()

        ADD_CUSTOM_TARGET (${DOCUMENTATION_TARGET} ALL
            COMMAND ${DOXYGEN_EXECUTABLE} ${DOXYFILE}
            WORKING_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}
            COMMENT "Generating API documentation with Doxygen"
            VERBATIM)
        
    ENDIF ()

    INSTALL (DIRECTORY "${DOCS_OUTPUT_PATH}/html/" DESTINATION ${INSTALL_DOC} COMPONENT "documentation")

ENDIF ()

### Configuration

CONFIGURE_FILE (
    "${CMAKE_MODULE_PATH}/${PROJECT_PATH}Config.cmake.in"
    "${CMAKE_CURRENT_BINARY_DIR}/${PROJECT_PATH}Config.cmake"
    @ONLY)

CONFIGURE_FILE (
    "${CMAKE_MODULE_PATH}/${PROJECT_PATH}ConfigVersion.cmake.in"
    "${CMAKE_CURRENT_BINARY_DIR}/${PROJECT_PATH}ConfigVersion.cmake"
    @ONLY)

INSTALL (FILES "${PROJECT_BINARY_DIR}/${PROJECT_PATH}Config.cmake" DESTINATION "${INSTALL_LIB}/${PROJECT_PATH}" COMPONENT "libraries")
INSTALL (FILES "${PROJECT_BINARY_DIR}/${PROJECT_PATH}ConfigVersion.cmake" DESTINATION "${INSTALL_LIB}/${PROJECT_PATH}" COMPONENT "libraries")

### Uninstall

CONFIGURE_FILE (
    "${CMAKE_MODULE_PATH}/UninstallTarget.cmake.in"
    "${CMAKE_CURRENT_BINARY_DIR}/UninstallTarget.cmake"
    IMMEDIATE @ONLY)

ADD_CUSTOM_TARGET ("uninstall" COMMAND ${CMAKE_COMMAND} -P "${CMAKE_CURRENT_BINARY_DIR}/UninstallTarget.cmake")

### Misc

INSTALL (FILES "LICENSE" DESTINATION ${INSTALL_ROOT} COMPONENT "documentation")

IF (INSTALL_DATA)

    IF (EXISTS "${PROJECT_SOURCE_DIR}/share/data")

        INSTALL (DIRECTORY "${PROJECT_SOURCE_DIR}/share/data" DESTINATION ${INSTALL_DATA} COMPONENT "data")

    ENDIF ()

ENDIF ()

########################################################################################################################################################

## Packaging

SET (CPACK_PACKAGE_NAME ${PROJECT_PACKAGE_NAME})
SET (CPACK_PACKAGE_DESCRIPTION_SUMMARY ${PROJECT_DESCRIPTION})
SET (CPACK_PACKAGE_DESCRIPTION_FILE "${PROJECT_SOURCE_DIR}/LICENSE")
SET (CPACK_PACKAGE_SUMMARY ${PROJECT_NAME})
SET (CPACK_PACKAGE_VENDOR ${PROJECT_VENDOR_NAME})
SET (CPACK_PACKAGE_CONTACT ${PROJECT_VENDOR_CONTACT})
SET (CPACK_PACKAGE_LICENSE ${PROJECT_LICENSE})
SET (CPACK_PACKAGE_VERSION ${PROJECT_VERSION_STRING})
SET (CPACK_PACKAGE_VERSION_MAJOR ${PROJECT_VERSION_MAJOR})
SET (CPACK_PACKAGE_VERSION_MINOR ${PROJECT_VERSION_MINOR})
SET (CPACK_PACKAGE_VERSION_PATCH ${PROJECT_VERSION_PATCH})
SET (CPACK_PACKAGE_RELEASE 1)
SET (CPACK_PACKAGE_FILE_NAME "${CPACK_PACKAGE_NAME}-${CPACK_PACKAGE_VERSION}-${CPACK_PACKAGE_RELEASE}.${CMAKE_SYSTEM_PROCESSOR}")

SET (CPACK_RESOURCE_FILE_LICENSE "${PROJECT_SOURCE_DIR}/LICENSE")
SET (CPACK_RESOURCE_FILE_README "${PROJECT_SOURCE_DIR}/README.md")

SET (CPACK_COMPONENTS_ALL "applications" "headers" "libraries" "documentation" "tests" "data")

SET (CPACK_COMPONENT_APPLICATIONS_DISPLAY_NAME "Applications")
SET (CPACK_COMPONENT_HEADERS_DISPLAY_NAME "Headers")
SET (CPACK_COMPONENT_LIBRARIES_DISPLAY_NAME "Libraries")
SET (CPACK_COMPONENT_DOCUMENTATION_DISPLAY_NAME "Documentation")
SET (CPACK_COMPONENT_TESTS_DISPLAY_NAME "Unit Tests")
SET (CPACK_COMPONENT_DATA_DISPLAY_NAME "Shared Data")
SET (CPACK_COMPONENT_PYTHON_DISPLAY_NAME "Python Bindings")

SET (CPACK_COMPONENT_APPLICATIONS_DESCRIPTION "Applications")
SET (CPACK_COMPONENT_HEADERS_DESCRIPTION "Headers")
SET (CPACK_COMPONENT_LIBRARIES_DESCRIPTION "Libraries")
SET (CPACK_COMPONENT_DOCUMENTATION_DESCRIPTION "Documentation")
SET (CPACK_COMPONENT_TESTS_DESCRIPTION "Unit Tests")
SET (CPACK_COMPONENT_DATA_DESCRIPTION "Shared Data")
SET (CPACK_COMPONENT_PYTHON_DESCRIPTION "Python Bindings")

SET (CPACK_COMPONENT_APPLICATIONS_DEPENDS "libraries")
SET (CPACK_COMPONENT_HEADERS_DEPENDS "libraries")
SET (CPACK_COMPONENT_LIBRARIES_DEPENDS "data")
SET (CPACK_COMPONENT_PYTHON_DEPENDS "libraries")

SET (CPACK_COMPONENT_APPLICATIONS_GROUP "runtime")
SET (CPACK_COMPONENT_HEADERS_GROUP "devel")
SET (CPACK_COMPONENT_LIBRARIES_GROUP "runtime")
SET (CPACK_COMPONENT_DOCUMENTATION_GROUP "devel")
SET (CPACK_COMPONENT_TESTS_GROUP "devel")
SET (CPACK_COMPONENT_DATA_GROUP "runtime")
SET (CPACK_COMPONENT_PYTHON_GROUP "python")

SET (CPACK_COMPONENT_GROUP_RUNTIME_DESCRIPTION "Runtime")
SET (CPACK_COMPONENT_GROUP_DEVEL_DESCRIPTION "Development tools")
SET (CPACK_COMPONENT_GROUP_PYTHON_DESCRIPTION "Python bindings")

SET (CPACK_PACKAGING_INSTALL_PREFIX ${CMAKE_INSTALL_PREFIX})

SET (CPACK_GENERATOR "RPM")

SET (CPACK_RPM_COMPONENT_INSTALL "ON")
SET (CPACK_RPM_PACKAGE_LICENSE ${CPACK_PACKAGE_LICENSE})
SET (CPACK_RPM_PACKAGE_VENDOR ${CPACK_PACKAGE_VENDOR})
SET (CPACK_RPM_PACKAGE_DESCRIPTION ${CPACK_PACKAGE_DESCRIPTION_SUMMARY})

# SET (CPACK_RPM_PACKAGE_REQUIRES "eigen3")

SET (CPACK_RPM_EXCLUDE_FROM_AUTO_FILELIST
    /usr
    /usr/bin
    /usr/include
    /usr/lib
    /usr/share
    /usr/local
    /usr/local/bin
    /usr/local/include
    /usr/local/lib
    /usr/local/share
    /usr/share/man
)

INCLUDE (CPack)

########################################################################################################################################################