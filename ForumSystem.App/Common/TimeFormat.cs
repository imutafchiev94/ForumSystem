using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.App.Common
{
    public class TimeFormat
    {
        public static string Posted(DateTime CreatedOn)
        {
            var date = DateTime.UtcNow - CreatedOn;

            var builder = new StringBuilder();

            if (date.Days > 365)
            {
                var years = date.Days % 365;
                if (years == 1)
                {
                    builder.Append($"{years} year ago");
                }
                else
                {
                    builder.Append($"{years} years ago");
                }

            }
            else if (date.Days > 30 && date.Days < 365)
            {
                var months = date.Days % 30;
                if (months == 1)
                {
                    builder.Append($"{months} month ago");
                }
                else
                {
                    builder.Append($"{months} months ago");
                }

            }
            else if (date.Days >= 1 && date.Days <= 30)
            {
                if (date.Days == 1)
                {
                    builder.Append($"{date.Days} day ago");
                }
                else
                {
                    builder.Append($"{date.Days} days ago");
                }

            }
            else if (date.Hours >= 1)
            {
                if (date.Hours == 1)
                {
                    builder.Append($"{date.Hours} hour ago");
                }
                else
                {
                    builder.Append($"{date.Hours} hours ago");
                }

            }
            else if (date.Minutes > 1)
            {
                builder.Append($"{date.Minutes} minutes ago");
            }
            else
            {
                builder.Append("a few seconds ago");
            }

            return builder.ToString();
        }
    }
}
