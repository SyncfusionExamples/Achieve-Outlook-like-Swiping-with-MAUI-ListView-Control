# Achieve-Outlook-like-Swiping-with-MAUI-ListView-Control

The .NET MAUI ListView control allows users swipe items to perform various actions, including deleting, adding, or editing data. By customizing the available features in ListView, you can create an inbox style similar to Outlook.

Swipe views improve user engagement by enabling fast, gesture-driven actions when swiping items left, right, up, or down. They can be customized to assign different functions to each swipe direction, such as deleting, archiving, or marking as read, depending on the design. 

The interface for these actions can be personalized with distinct colors, icons, and text, offering instant visual feedback through animations. Swipe views are frequently utilized in list-oriented applications like email clients, messaging platforms, and to-do lists.

This sample demonstrates how to achieve Outlook-like Swiping in the MAUI ListView control within a .NET MAUI application. You'll also see the process of setting up an Outlook-style swipe feature, which allows users to archive and delete messages, as well as display pop-up notification for these actions, complete with an Undo option.

## Sample

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="40" />
        <RowDefinition Height="*" />
    </Grid.RowDefinitions>
    <Label Grid.Row="0"
           Text="Inbox"
           TextColor="Black"
           FontSize="18"
           FontFamily="Roboto-Medium"
           Margin="16,0,0,0"
           VerticalOptions="Center" />
    <listView:SfListView Grid.Row="1"
                         x:Name="listView"
                         ItemsSource="{Binding InboxInfos}"
                         ItemSize="70"
                         AutoFitMode="Height"
                         AllowSwiping="True"
                         SwipeEnded="listView_SwipeEnded"
                         SwipeOffset="500">
        <listView:SfListView.StartSwipeTemplate>
            <DataTemplate>
                <Grid BackgroundColor="#D8F3D4">
                    <Label Text="&#xe71C;"
                           FontFamily='{OnPlatform Android=ListViewFontIcons.ttf#,WinUI=ListViewFontIcons.ttf#ListViewFontIcons}'
                           TextColor="Green"
                           HorizontalOptions="Center"
                           VerticalOptions="Center"
                           FontAttributes="Bold"
                           FontSize="22"/>
                </Grid>
            </DataTemplate>
        </listView:SfListView.StartSwipeTemplate>
        <listView:SfListView.EndSwipeTemplate>
            <DataTemplate>
                <Grid BackgroundColor="#F4DEDE">
                    <Label Text="&#xe716;"
                           FontFamily='{OnPlatform Android=ListViewFontIcons.ttf#,WinUI=ListViewFontIcons.ttf#ListViewFontIcons}'
                           TextColor="DarkRed"
                           HorizontalOptions="Center"
                           VerticalOptions="Center"
                           FontSize="26">
                    </Label>
                </Grid>
            </DataTemplate>
        </listView:SfListView.EndSwipeTemplate>
        <listView:SfListView.ItemTemplate>
            <DataTemplate>
                <Grid RowDefinitions="5,20,20,20,5" ColumnDefinitions="72,*,Auto">
                    <Grid Grid.Row="1"
                          Grid.Column="0"
                          Grid.RowSpan="2"
                          HeightRequest="35"
                          WidthRequest="35"
                          HorizontalOptions="Center"
                          VerticalOptions="Center">
                        <Image Source="{Binding Image}"
                               HeightRequest="40"
                               WidthRequest="40"
                               Margin="0, 15, 0, 0" />
                        <Label Text="{Binding ProfileName}"
                               TextColor="#FFFFFF"
                               FontSize="14"
                               HorizontalTextAlignment="Center"
                               HorizontalOptions="Center"
                               VerticalOptions="Center"
                               VerticalTextAlignment="Center"
                               FontFamily="Roboto-Regular"
                               CharacterSpacing="0.25"
                               Margin="0, 15, 0, 0" />
                    </Grid>

                    <Label Grid.Row="1"
                           Grid.Column="1"
                           Text="{Binding Name}"
                           FontFamily="Roboto-Medium"
                           FontSize="14"
                           TextColor="#000000"
                           Margin="0, 2, 0, 0"
                           LineBreakMode="TailTruncation"
                           CharacterSpacing="0.25" />

                    <Label Grid.Row="2"
                           Grid.Column="1"
                           Grid.ColumnSpan="2"
                           Text="{Binding Subject}"
                           FontFamily="Roboto-Medium"
                           FontSize="13"
                           Margin="0,0,25,3"
                           TextColor="#000000"
                           LineBreakMode="TailTruncation"
                           CharacterSpacing="0.25" />

                    <Label Grid.Row="3"
                           Grid.Column="1"
                           Grid.ColumnSpan="2"
                           Text="{Binding Description}"
                           FontFamily="Roboto-Regular"
                           FontSize="12"
                           TextColor="#666666"
                           Margin="0,0,16,1"
                           LineBreakMode="TailTruncation"
                           CharacterSpacing="0.25" />

                    <Image Grid.Row="2"
                           Grid.Column="2"
                           HeightRequest="30"
                           WidthRequest="30"
                           Margin="0, 0, 8, 0"
                           Source="paperclip.png"
                           IsVisible="{Binding IsAttached}"
                           HorizontalOptions="End"
                           VerticalOptions="Center">
                    </Image>

                    <Image Grid.Row="2"
                           Grid.Column="2"
                           HeightRequest="40"
                           WidthRequest="40"
                           Margin="0, 0, 2, 0"
                           Source="important.png"
                           IsVisible="{Binding IsImportant}"
                           HorizontalOptions="End"
                           VerticalOptions="Center">
                    </Image>
                </Grid>
            </DataTemplate>
        </listView:SfListView.ItemTemplate>
    </listView:SfListView>
    
    <Frame Grid.Row="1"
           CornerRadius="4"
           Margin="16,0,16,5"
           Padding="0"
           HeightRequest="40"
           VerticalOptions="End"
           IsVisible="{Binding IsDeleted}">
        <Grid HeightRequest="40" BackgroundColor="#3D454A">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="Auto" />
            </Grid.ColumnDefinitions>
            <Label Grid.Column="0"
                   Text="{Binding PopUpText}"
                   TextColor="#FFFFFF"
                   FontSize="14"
                   VerticalOptions="Center"
                   VerticalTextAlignment="Center"
                   Margin="16,0,0,0"
                   CharacterSpacing="0.25"/>
            <Label Grid.Column="1"
                   Text="Undo"
                   FontSize="14"
                   HorizontalOptions="End"
                   TextColor="#1796E6"
                   VerticalOptions="Center"
                   VerticalTextAlignment="Center"
                   Margin="0,0,16,0"
                   CharacterSpacing="0.25">
                <Label.GestureRecognizers>
                    <TapGestureRecognizer Command="{Binding UndoCommand}"/>
                </Label.GestureRecognizers>
            </Label>
        </Grid>
    </Frame>
</Grid>
```

## Requirements to run the demo

To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)

## Troubleshooting:
### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion's samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion's samples.