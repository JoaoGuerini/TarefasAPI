﻿using System.ComponentModel;

namespace TarefasApi.Enums
{
    public enum StatusTarefa
    {
        [Description("A fazer")]
        AFazer = 1,

        [Description("Em andamento")]
        EmAndamento = 2,

        [Description("Concluido")]
        Concluido = 3,
    }
}
