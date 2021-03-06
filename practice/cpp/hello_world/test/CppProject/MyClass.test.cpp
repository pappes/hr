////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///
///	Copyright (C) 2018 by pappes
///
///	This file is part of the hw project.
///
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

///	@file						CppProject/MyClass.test.cpp
///	@author						pappes <pappes@gmail.com>
///	@date						9 Jun 2018

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#include <Global.test.hpp>

#include <CppProject/MyClass.hpp>

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

TEST(CppProject_MyClass, IsDefined)
{

	using cppproject::MyClass ;

	{

		MyClass myClass ;

		EXPECT_FALSE(myClass.isDefined()) ;

	}

	{

		MyClass myClass = MyClass::Integer(123) ;

		EXPECT_TRUE(myClass.isDefined()) ;

	}

}

TEST(CppProject_MyClass, DoSomething)
{

	using cppproject::MyClass ;

	{

		MyClass myClass ;

		myClass.doSomething() ;

		EXPECT_EQ(0, myClass.getInteger()) ;

	}

	{

		MyClass myClass = MyClass::Integer(123) ;

		myClass.doSomething() ;

		EXPECT_EQ(246, myClass.getInteger()) ;

	}

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////