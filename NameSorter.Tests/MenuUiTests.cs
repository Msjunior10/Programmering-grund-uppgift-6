using NameSorter;
using Xunit;

namespace NameSorter.Tests;

public sealed class MenuUiTests
{
	[Theory]
	[InlineData("1", 1)]
	[InlineData("2", 2)]
	[InlineData("3", 3)]
	[InlineData("4", 4)]
	public void TryParseMenuAction_ReturnsExpectedAction(string input, int expectedAction)
	{
		bool result = MenuUi.TryParseMenuAction(input, out MenuAction action);

		Assert.True(result);
		Assert.Equal((MenuAction)expectedAction, action);
	}

	[Theory]
	[InlineData("")]
	[InlineData("hej")]
	[InlineData("9")]
	public void TryParseMenuAction_ReturnsFalseForInvalidInput(string input)
	{
		bool result = MenuUi.TryParseMenuAction(input, out MenuAction action);

		Assert.False(result);
		Assert.Equal(default, action);
	}
}