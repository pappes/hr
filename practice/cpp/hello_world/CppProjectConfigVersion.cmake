########################################################################################################################################################
###
### Copyright (C) 2018 by N/A
###
### This file is part of the hw project.
###
########################################################################################################################################################

### @file                       CppProject/tools/cmake/CppProjectConfigVersion.cmake.in
### @author                     pappes <pappes@gmail.com>
### @date                       9 Jun 2018

########################################################################################################################################################

SET (PACKAGE_VERSION "2018.6.12")
SET (PACKAGE_VERSION_MAJOR "2018")
SET (PACKAGE_VERSION_MINOR "6")
SET (PACKAGE_VERSION_PATCH "12")

IF (NOT PACKAGE_FIND_NAME STREQUAL "hw")

	# Check package name (in particular, because of the way cmake finds
	# package config files, the capitalization could easily be "wrong").
	# This is necessary to ensure that the automatically generated
	# variables, e.g., <package>_FOUND, are consistently spelled.
	
	SET (REASON "package = hw, NOT ${PACKAGE_FIND_NAME}")
	SET (PACKAGE_VERSION_UNSUITABLE TRUE)

ELSEIF (NOT (APPLE OR (NOT DEFINED CMAKE_SIZEOF_VOID_P) OR CMAKE_SIZEOF_VOID_P EQUAL 4))

	# Reject if there's a 32-bit/64-bit mismatch (not necessary with Apple
	# since a multi-architecture library is built for that platform).
	
	SET (REASON "sizeof(*void) = 4")
	SET (PACKAGE_VERSION_UNSUITABLE TRUE)

ELSEIF (MSVC AND NOT MSVC_VERSION STREQUAL "1911")

	# Reject if there's a mismatch in MSVC compiler versions
	
	SET (REASON "_MSC_VER = 1911")
	SET (PACKAGE_VERSION_UNSUITABLE TRUE)

ELSEIF (NOT CMAKE_CROSSCOMPILING STREQUAL "FALSE")

	# Reject if there's a mismatch in ${CMAKE_CROSSCOMPILING}
	
	SET (REASON "cross-compiling = FALSE")
	SET (PACKAGE_VERSION_UNSUITABLE TRUE)

ELSEIF (CMAKE_CROSSCOMPILING AND NOT (CMAKE_SYSTEM_NAME STREQUAL "Windows" AND CMAKE_SYSTEM_PROCESSOR STREQUAL "AMD64"))

	# Reject if cross-compiling and there's a mismatch in the target system
	
	SET (REASON "target = Windows-AMD64")
	SET (PACKAGE_VERSION_UNSUITABLE TRUE)

ELSEIF (PACKAGE_FIND_VERSION)

	IF (PACKAGE_FIND_VERSION VERSION_EQUAL PACKAGE_VERSION)

		SET (PACKAGE_VERSION_EXACT TRUE)
	
	ELSEIF (PACKAGE_FIND_VERSION VERSION_LESS PACKAGE_VERSION AND PACKAGE_FIND_VERSION_MAJOR EQUAL PACKAGE_VERSION_MAJOR)

		SET (PACKAGE_VERSION_COMPATIBLE TRUE)
	
	ENDIF ()

ENDIF ()

# If unsuitable, append the reason to the package version so that it's visible to the user.

IF (PACKAGE_VERSION_UNSUITABLE)
	
	SET (PACKAGE_VERSION "${PACKAGE_VERSION} (${REASON})")

ENDIF ()

########################################################################################################################################################
