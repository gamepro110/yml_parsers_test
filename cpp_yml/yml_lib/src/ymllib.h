#pragma once

#include <yaml-cpp/yaml.h>

#include <iostream>
#include <string>
#include <vector>
#include <memory>
#include <tuple>
#include <type_traits>

struct ItemBase {
	typedef const std::string(*ToYml)(ItemBase*);

	template<typename T>
	typedef T(*FromYml)(const std::string& yml);
	
	ItemBase(ToYml _fn) :
		function(_fn)
	{ }

	void Invoke() {
		function(this);
	}

	ToYml function{ nullptr };
};

template<typename T>
class ItemParam : ItemBase {
	template<typename T>
	typedef const std::string(*ToYml)(T value);

public:
	template<typename T>
	ItemParam(ToYml _fn, T* _item) :
		ItemBase([](ItemBase* _base) { static_cast<ItemBase*>(_base)->Invoke(); }),
		function(_fn),
		item(_item)
	{ }

private:
	void Invoke() {
		std::apply(function, &item);
	}

private:
	ToYml function{ nullptr };
	T* item;
};

class Person {
public:
	Person() {
	}

	~Person() {
	}

	const std::string ToYml() {
		std::string str;
		std::cout << "[yml]:serializing\n";
		return str;
	};


	const Person FromYml(const std::string& yml) {
		Person p;
		std::cout << "[yml]:deserializing\n";
		return p;
	};

private:
	std::string name;
	int age;
	double salery;
	std::vector<int> tasks;
};

class YmlLib {
public:
	YmlLib();
	~YmlLib();

	template<typename T>
	std::string ToYml(T thing) {
		YmlConvertable convert{T};
		std::cout << "... " << convert.ToYml() << "\n";
		
		YAML::Emitter out;
		out << thing;
		return out.c_str();
	}

	template<typename T>
	const T& FromYml(const std::string& yml) {
		return T;
	}
};