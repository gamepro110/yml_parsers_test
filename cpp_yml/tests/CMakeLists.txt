# tests config

file(
	GLOB_RECURSE
	tests_SRC
	"src/*.cpp"
	"src/*.h"
)

add_executable(
	tests
	"${PROJECT_SOURCE_DIR}"
)

target_link_libraries(
	tests
	PUBLIC
	ymlLib
)

target_include_directories(
	tests
	PUBLIC
	"${PROJECT_SOURCE_DIR}/yml_lib/src/"
)