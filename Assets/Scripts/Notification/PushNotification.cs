using System;
using Unity.Notifications.Android;
using UnityEngine;

public class PushNotification : MonoBehaviour
{
    public void Initialize()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Name = "����������� � ������������",
            Description = "����� ���� ������������ �� �������",
            Id = "horoscope",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void MakingNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();

        DateTime dateTime = DateTime.Now;
        dateTime = DateTime.Parse($"{dateTime.Day}.{dateTime.Month}.{dateTime.Year} 11:00:00");

        for (int i = 1; i < 30; i++)
        {
            dateTime = dateTime.AddDays(1);

            AndroidNotification notification = new AndroidNotification()
            {
                Title = "����� ���� �������",
                Text = "����� ���� ������������ �� �������",
                FireTime = dateTime
            };

            AndroidNotificationCenter.SendNotification(notification, "horoscope");
        }
    }
}
