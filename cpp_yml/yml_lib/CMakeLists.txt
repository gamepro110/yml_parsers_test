# yml_lib config

file(
	GLOB_RECURSE
	YMLLIB_SRC
	"src/*.cpp"
	"src/*.h"
)

add_library(
	ymlLib
	STATIC
	"${YMLLIB_SRC}"
)


target_link_libraries(
	ymlLib
	PUBLIC
	yaml-cpp
)

target_include_directories(
	ymlLib
	PUBLIC
	"${vendorPath}/yaml-cpp/include/"
)