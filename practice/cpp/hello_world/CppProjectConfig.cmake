########################################################################################################################################################
###
### Copyright (C) 2018 by N/A
###
### This file is part of the hw project.
###
########################################################################################################################################################

### @file                       CppProject/tools/cmake/CppProjectConfig.cmake.in
### @author                     pappes <pappes@gmail.com>
### @date                       9 Jun 2018

########################################################################################################################################################

SET (CPP_PROJECT_TEMPLATE_ROOT_DIR ${CPP_PROJECT_TEMPLATE_ROOT_DIR} C:/Program Files (x86)/hw)

FIND_PATH (CPP_PROJECT_TEMPLATE_INCLUDE_DIR "CppProject/Utilities/Version.hpp" PATHS ${CPP_PROJECT_TEMPLATE_ROOT_DIR} PATH_SUFFIXES "include" NO_DEFAULT_PATH)
FIND_LIBRARY (CPP_PROJECT_TEMPLATE_LIBRARY NAMES "libhw-test.so" PATHS ${CPP_PROJECT_TEMPLATE_ROOT_DIR} PATH_SUFFIXES "lib" NO_DEFAULT_PATH)

# MESSAGE (STATUS "CPP_PROJECT_TEMPLATE_ROOT_DIR = ${CPP_PROJECT_TEMPLATE_ROOT_DIR}")
# MESSAGE (STATUS "CPP_PROJECT_TEMPLATE_INCLUDE_DIR = ${CPP_PROJECT_TEMPLATE_INCLUDE_DIR}")
# MESSAGE (STATUS "CPP_PROJECT_TEMPLATE_LIBRARY = ${CPP_PROJECT_TEMPLATE_LIBRARY}")
# MESSAGE (STATUS "CPP_PROJECT_TEMPLATE_FIND_VERSION = ${CPP_PROJECT_TEMPLATE_FIND_VERSION}")

IF (CPP_PROJECT_TEMPLATE_INCLUDE_DIR)

	SET (CPP_PROJECT_TEMPLATE_INCLUDE_DIRS ${CPP_PROJECT_TEMPLATE_INCLUDE_DIR} ${CPP_PROJECT_TEMPLATE_INCLUDE_DIR}/CppProject)
	SET (CPP_PROJECT_TEMPLATE_LIBRARIES ${CPP_PROJECT_TEMPLATE_LIBRARY})

	SET (CPP_PROJECT_TEMPLATE_FOUND TRUE)

ENDIF ()

########################################################################################################################################################
