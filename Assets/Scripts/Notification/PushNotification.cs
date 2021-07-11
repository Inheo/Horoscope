using System;
using Unity.Notifications.Android;
using UnityEngine;

public class PushNotification : MonoBehaviour
{
    public void Initialize()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Name = "Напоминание о предсказании",
            Description = "Узнай свое предсказание на сегодня",
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
                Title = "Узнай свое будущее",
                Text = "Узнай свое предсказание на сегодня",
                FireTime = dateTime
            };

            AndroidNotificationCenter.SendNotification(notification, "horoscope");
        }
    }
}
