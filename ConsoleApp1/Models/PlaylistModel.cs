using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class PlaylistModel
    {
        public string Tracker;
        public List<Category> List;
    }

    public class Category
    {
        public int Id;
        public string Name;
        public string Image;
        public List<Channel> ChannelList;
    }

    public class Channel
    {
        public int Id;
        public string Name;
        public string SwarmId;
        public string Description;
        public string Image;
        public string ImageAndr;
        public string Epg;
        public ProgramInfo CurrentProgramInfo;
    }

    public class ProgramInfo
    {
        public string Title;
        public string Description;
        public string StartTime;
        public string EndTime;
    }
}
