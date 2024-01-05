#include "stdafx.h"

constexpr std::array<std::string_view, 4> dummyText = {
	"The quick brown fox jumps over the lazy dog.",
	"She sells seashells by the seashore.",
	"How can a clam cram in a clean cream can?",
	"Fuzzy fuzzy was a bear, fuzzy fuzzy had no hair.",
};

void Main()
{
	ChildProcess childLogger{
		U"../../EasyLogger/bin/Release/net7.0-windows/EasyLogger.exe",
		Pipe::StdInOut
	};

	double t{};
	int count{};

	Print(U"Started 🚀");
	while (System::Update())
	{
		ClearPrint();
		Print(count);

		childLogger.ostream() << "#Tick" << std::endl;
		childLogger.ostream() << fmt::format("Count: {}", count) << std::endl;
		count++;

		childLogger.ostream() << "#Clear Dummy" << std::endl;
		for (int i = 0; i < 50; ++i)
		{
			childLogger.ostream() << "#Dummy " << dummyText[Random(dummyText.size() - 1)] << std::endl;
		}
		childLogger.ostream() << "50" << std::endl;
		for (int i = 0; i < 50; ++i)
		{
			childLogger.ostream() << "#Dummy " << dummyText[Random(dummyText.size() - 1)] << std::endl;
		}

		childLogger.ostream() << "Happy" << std::endl;

		t += Scene::DeltaTime();
		if (t > 1.0)
		{
			t = 0;
			childLogger.ostream() << "Hello C# WPF from C++ Siv3D" << std::endl;
		}
	}

	childLogger.terminate();
}
