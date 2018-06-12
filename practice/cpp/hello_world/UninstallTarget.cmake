########################################################################################################################################################
###
### Copyright (C) 2018 by N/A
###
### This file is part of the hw project.
###
########################################################################################################################################################

### @file                       CppProject/tools/cmake/UninstallTarget.cmake.in
### @author                     pappes <pappes@gmail.com>
### @date                       9 Jun 2018

########################################################################################################################################################

if(NOT EXISTS "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/install_manifest.txt")
  message(FATAL_ERROR "Cannot find install manifest: C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/install_manifest.txt")
endif(NOT EXISTS "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/install_manifest.txt")

file(READ "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/install_manifest.txt" files)
string(REGEX REPLACE "\n" ";" files "${files}")
foreach(file ${files})
  message(STATUS "Uninstalling $ENV{DESTDIR}${file}")
  if(IS_SYMLINK "$ENV{DESTDIR}${file}" OR EXISTS "$ENV{DESTDIR}${file}")
    exec_program(
      "C:/tools/cmake-3.11.3-win64-x64/bin/cmake.exe" ARGS "-E remove \"$ENV{DESTDIR}${file}\""
      OUTPUT_VARIABLE rm_out
      RETURN_VALUE rm_retval
      )
    if(NOT "${rm_retval}" STREQUAL 0)
      message(FATAL_ERROR "Problem when removing $ENV{DESTDIR}${file}")
    endif(NOT "${rm_retval}" STREQUAL 0)
  else(IS_SYMLINK "$ENV{DESTDIR}${file}" OR EXISTS "$ENV{DESTDIR}${file}")
    message(STATUS "File $ENV{DESTDIR}${file} does not exist.")
  endif(IS_SYMLINK "$ENV{DESTDIR}${file}" OR EXISTS "$ENV{DESTDIR}${file}")
endforeach(file)

########################################################################################################################################################
