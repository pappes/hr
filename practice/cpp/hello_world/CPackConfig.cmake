# This file will be configured to contain variables for CPack. These variables
# should be set in the CMake list file of the project before CPack module is
# included. The list of available CPACK_xxx variables and their associated
# documentation may be obtained using
#  cpack --help-variable-list
#
# Some variables are common to all generators (e.g. CPACK_PACKAGE_NAME)
# and some are specific to a generator
# (e.g. CPACK_NSIS_EXTRA_INSTALL_COMMANDS). The generator specific variables
# usually begin with CPACK_<GENNAME>_xxxx.


set(CPACK_BINARY_7Z "")
set(CPACK_BINARY_BUNDLE "")
set(CPACK_BINARY_CYGWIN "")
set(CPACK_BINARY_DEB "")
set(CPACK_BINARY_DRAGNDROP "")
set(CPACK_BINARY_FREEBSD "")
set(CPACK_BINARY_IFW "")
set(CPACK_BINARY_NSIS "")
set(CPACK_BINARY_OSXX11 "")
set(CPACK_BINARY_PACKAGEMAKER "")
set(CPACK_BINARY_PRODUCTBUILD "")
set(CPACK_BINARY_RPM "")
set(CPACK_BINARY_STGZ "")
set(CPACK_BINARY_TBZ2 "")
set(CPACK_BINARY_TGZ "")
set(CPACK_BINARY_TXZ "")
set(CPACK_BINARY_TZ "")
set(CPACK_BINARY_WIX "")
set(CPACK_BINARY_ZIP "")
set(CPACK_BUILD_SOURCE_DIRS "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world;C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world")
set(CPACK_CMAKE_GENERATOR "Visual Studio 15 2017")
set(CPACK_COMPONENTS_ALL "applications;headers;libraries;documentation;tests;data")
set(CPACK_COMPONENTS_ALL_SET_BY_USER "TRUE")
set(CPACK_COMPONENT_APPLICATIONS_DEPENDS "libraries")
set(CPACK_COMPONENT_APPLICATIONS_DESCRIPTION "Applications")
set(CPACK_COMPONENT_APPLICATIONS_DISPLAY_NAME "Applications")
set(CPACK_COMPONENT_APPLICATIONS_GROUP "runtime")
set(CPACK_COMPONENT_DATA_DESCRIPTION "Shared Data")
set(CPACK_COMPONENT_DATA_DISPLAY_NAME "Shared Data")
set(CPACK_COMPONENT_DATA_GROUP "runtime")
set(CPACK_COMPONENT_DOCUMENTATION_DESCRIPTION "Documentation")
set(CPACK_COMPONENT_DOCUMENTATION_DISPLAY_NAME "Documentation")
set(CPACK_COMPONENT_DOCUMENTATION_GROUP "devel")
set(CPACK_COMPONENT_GROUP_DEVEL_DESCRIPTION "Development tools")
set(CPACK_COMPONENT_GROUP_PYTHON_DESCRIPTION "Python bindings")
set(CPACK_COMPONENT_GROUP_RUNTIME_DESCRIPTION "Runtime")
set(CPACK_COMPONENT_HEADERS_DEPENDS "libraries")
set(CPACK_COMPONENT_HEADERS_DESCRIPTION "Headers")
set(CPACK_COMPONENT_HEADERS_DISPLAY_NAME "Headers")
set(CPACK_COMPONENT_HEADERS_GROUP "devel")
set(CPACK_COMPONENT_LIBRARIES_DEPENDS "data")
set(CPACK_COMPONENT_LIBRARIES_DESCRIPTION "Libraries")
set(CPACK_COMPONENT_LIBRARIES_DISPLAY_NAME "Libraries")
set(CPACK_COMPONENT_LIBRARIES_GROUP "runtime")
set(CPACK_COMPONENT_PYTHON_DEPENDS "libraries")
set(CPACK_COMPONENT_PYTHON_DESCRIPTION "Python Bindings")
set(CPACK_COMPONENT_PYTHON_DISPLAY_NAME "Python Bindings")
set(CPACK_COMPONENT_PYTHON_GROUP "python")
set(CPACK_COMPONENT_TESTS_DESCRIPTION "Unit Tests")
set(CPACK_COMPONENT_TESTS_DISPLAY_NAME "Unit Tests")
set(CPACK_COMPONENT_TESTS_GROUP "devel")
set(CPACK_COMPONENT_UNSPECIFIED_HIDDEN "TRUE")
set(CPACK_COMPONENT_UNSPECIFIED_REQUIRED "TRUE")
set(CPACK_GENERATOR "RPM")
set(CPACK_INSTALL_CMAKE_PROJECTS "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world;hw;ALL;/")
set(CPACK_INSTALL_PREFIX "C:/Program Files (x86)/hw")
set(CPACK_MODULE_PATH "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/tools/cmake")
set(CPACK_NSIS_DISPLAY_NAME "hw-test 2018.06.12")
set(CPACK_NSIS_INSTALLER_ICON_CODE "")
set(CPACK_NSIS_INSTALLER_MUI_ICON_CODE "")
set(CPACK_NSIS_INSTALL_ROOT "$PROGRAMFILES")
set(CPACK_NSIS_PACKAGE_NAME "hw-test 2018.06.12")
set(CPACK_OUTPUT_CONFIG_FILE "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/CPackConfig.cmake")
set(CPACK_PACKAGE_CONTACT "pappes@gmail.com")
set(CPACK_PACKAGE_DEFAULT_LOCATION "/")
set(CPACK_PACKAGE_DESCRIPTION_FILE "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/LICENSE")
set(CPACK_PACKAGE_DESCRIPTION_SUMMARY "compiler test")
set(CPACK_PACKAGE_FILE_NAME "hw-test-2018.06.12-1.AMD64")
set(CPACK_PACKAGE_INSTALL_DIRECTORY "hw-test 2018.06.12")
set(CPACK_PACKAGE_INSTALL_REGISTRY_KEY "hw-test 2018.06.12")
set(CPACK_PACKAGE_LICENSE "MIT License")
set(CPACK_PACKAGE_NAME "hw-test")
set(CPACK_PACKAGE_RELEASE "1")
set(CPACK_PACKAGE_RELOCATABLE "true")
set(CPACK_PACKAGE_SUMMARY "hw")
set(CPACK_PACKAGE_VENDOR "pappes")
set(CPACK_PACKAGE_VERSION "2018.06.12")
set(CPACK_PACKAGE_VERSION_MAJOR "2018")
set(CPACK_PACKAGE_VERSION_MINOR "6")
set(CPACK_PACKAGE_VERSION_PATCH "12")
set(CPACK_PACKAGING_INSTALL_PREFIX "C:/Program Files (x86)/hw")
set(CPACK_RESOURCE_FILE_LICENSE "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/LICENSE")
set(CPACK_RESOURCE_FILE_README "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/README.md")
set(CPACK_RESOURCE_FILE_WELCOME "C:/tools/cmake-3.11.3-win64-x64/share/cmake-3.11/Templates/CPack.GenericWelcome.txt")
set(CPACK_RPM_COMPONENT_INSTALL "ON")
set(CPACK_RPM_EXCLUDE_FROM_AUTO_FILELIST "/usr;/usr/bin;/usr/include;/usr/lib;/usr/share;/usr/local;/usr/local/bin;/usr/local/include;/usr/local/lib;/usr/local/share;/usr/share/man")
set(CPACK_RPM_PACKAGE_DESCRIPTION "compiler test")
set(CPACK_RPM_PACKAGE_LICENSE "MIT License")
set(CPACK_RPM_PACKAGE_VENDOR "pappes")
set(CPACK_SET_DESTDIR "OFF")
set(CPACK_SOURCE_7Z "ON")
set(CPACK_SOURCE_CYGWIN "")
set(CPACK_SOURCE_GENERATOR "7Z;ZIP")
set(CPACK_SOURCE_OUTPUT_CONFIG_FILE "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/CPackSourceConfig.cmake")
set(CPACK_SOURCE_RPM "")
set(CPACK_SOURCE_TBZ2 "")
set(CPACK_SOURCE_TGZ "")
set(CPACK_SOURCE_TXZ "")
set(CPACK_SOURCE_TZ "")
set(CPACK_SOURCE_ZIP "ON")
set(CPACK_SYSTEM_NAME "win32")
set(CPACK_TOPLEVEL_TAG "win32")
set(CPACK_WIX_SIZEOF_VOID_P "4")

if(NOT CPACK_PROPERTIES_FILE)
  set(CPACK_PROPERTIES_FILE "C:/Users/dj/Documents/GitHub/hr/practice/cpp/hello_world/CPackProperties.cmake")
endif()

if(EXISTS ${CPACK_PROPERTIES_FILE})
  include(${CPACK_PROPERTIES_FILE})
endif()
