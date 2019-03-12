using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net
{
    /// <summary>网络传输数据类</summary>
    public class SocketMessage
    {
        /// <summary>一级模块</summary>
        public int ModuleType { get; set; }
        /// <summary>二级模块</summary>
        public int MessageType { get; set; }
        /// <summary>消息内容</summary>
        public string Message { get; set; }
        /// <summary>信息长度</summary>
        public int Length { get; set; }

        public SocketMessage(int moduleType,int messageType,string message)
        {
            ModuleType = moduleType;
            MessageType = messageType;
            Message = message;
            
            //Length的字节数,ModuleType的字节数,MessageType的字节数,系统自动添加的储存字符串长度的字节,Message的字节数
            Length = 4 + 4 + 4 + 1 + Encoding.UTF8.GetBytes(message).Length;
        }
    }
}
