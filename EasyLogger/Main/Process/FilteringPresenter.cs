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
        setupAllTag();

        CheckAddFilter(ReservedTags.Info);
        CheckAddFilter(ReservedTags.Warn);
        CheckAddFilter(ReservedTags.Error);
    }

    private void setupAllTag()
    {
        var allTag = "All";
        Model.IsAllFiltered = false;
        var allFilter = new CheckBox()
        {
            Content = new TextBlock()
            {
                Text = allTag,
                Foreground = System.Windows.Media.Brushes.LightCyan
            },
            IsChecked = false,
        };
        allFilter.Click += (sender, args) =>
        {
            Model.IsAllFiltered = allFilter.IsChecked ?? false;
            foreach (var child in FilterStack.Children)
            {
                if (child is not CheckBox checkBox) continue;
                if (checkBox == allFilter) continue;
                Model.SetFiltered(checkBox.Content as string ?? "", Model.IsAllFiltered);
                checkBox.IsChecked = Model.IsAllFiltered;
                // checkBox.Visibility = allFilter.IsChecked.Value ? Visibility.Hidden : Visibility.Visible;
            }

            applyList(LoggingList);
        };
        FilterStack.Children.Add(allFilter);
    }

    public void AppleElement(LoggedElement element)
    {
        bool isVisible = (Model.IsFiltered(element.Tag));
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