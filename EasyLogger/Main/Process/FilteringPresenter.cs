#nullable enable

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SimpleLogger.Main.Data;
using SimpleLogger.Main.Present;

namespace SimpleLogger.Main.Process;

public record FilteringProcess(
    FilteringModel Model,
    StackPanel FilterStack,
    LoggingList LoggingList)
{
    public void Setup()
    {
        var allTag = "All";
        Model.IsAllFiltered = false;
        var allFilter = new CheckBox()
        {
            Content = allTag,
            IsChecked = false
        };
        allFilter.Click += (sender, args) =>
        {
            Model.IsAllFiltered = allFilter.IsChecked ?? false;
            applyList(LoggingList);
            foreach (var child in FilterStack.Children)
            {
                if (child is not CheckBox checkBox) continue;
                if (checkBox == allFilter) continue;
                checkBox.Visibility = allFilter.IsChecked.Value ? Visibility.Hidden : Visibility.Visible;
            }
        };
        FilterStack.Children.Add(allFilter);
    }

    public void AppleElement(LoggedElement element)
    {
        bool isVisible = Model.IsAllFiltered || (Model.IsFiltered(element.Tag));
        element.LoggingLine.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    public void CheckAddFilter(string tag)
    {
        if (Model.Tags.Contains(tag)) return;

        Model.SetFiltered(tag, true);
        var newFilter = new CheckBox()
        {
            Content = tag,
            IsChecked = true
        };
        newFilter.Click += (sender, args) =>
        {
            Model.SetFiltered(tag, newFilter.IsChecked ?? false);
            applyList(LoggingList);
        };
        FilterStack.Children.Add(newFilter);
    }

    private void applyList(LoggingList list)
    {
        foreach (var element in list.Data)
        {
            AppleElement(element);
        }
    }
}