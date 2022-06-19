using System;
using System.Collections.Generic;
using System.Text;

namespace VRisingExpanded.Server.Commands
{
    public interface ICommand

    {
        /// <summary>
        /// 命令名称。
        /// </summary>
        string Keys { get; }

        /// <summary>
        /// 使用样例
        /// </summary>
        string Usage { get; }

        /// <summary>
        /// 命令简介。
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 只有管理员才能用
        /// </summary>
        bool AdminOnly { get; }

        /// <summary>
        /// 执行
        /// </summary>
        void Initialize(CommandContext context);
    }
}