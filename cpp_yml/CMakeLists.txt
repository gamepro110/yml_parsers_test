cmake_minimum_required(VERSION 3.16)

project(cpp_yml)

set(CXX_STANDARD 20)
set(CXX_STANDARD_REQUIRED TRUE)

set(vendorPath "${PROJECT_SOURCE_DIR}/vendor")

add_subdirectory(vendor)
add_subdirectory(yml_lib)
add_subdirectory(sender)
add_subdirectory(reciever)
add_subdirectory(tests)