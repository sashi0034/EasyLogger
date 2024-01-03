#include "stdafx.h"

void Main()
{
	ChildProcess childLogger{U"../../SimpleLogger/bin/Release/net7.0-windows/SimpleLogger.exe", Pipe::StdInOut};

	double t{};

	Print(U"Started 🚀");
	while (System::Update())
	{
		t += Scene::DeltaTime();
		if (t > 0.5)
		{
			t = 0;
			childLogger.ostream() << "Hello C# WPF from C++ Siv3D" << std::endl;
		}
	}

	childLogger.terminate();
}
