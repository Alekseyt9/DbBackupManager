{
    tasks: [
        {
            name: "LangSequenceTraining_backup",
            source: {
                provider: "postgres",
                properties: {
                    dump_path: "",
                    base: "LangSequenceTraining"
                }                
            },            
            destination: {
                provider: "yandex_disk",
                properties: {
                    token: "y0_AgAAAAALO2EBAAoi3wAAAADnCMTWyUDoupLASx2guxg8GITw4V8EA0s",
                    path: "backups/LangSequenceTraining/",                   
                    keepCount: 10
                },               
            },
            schedule: {
                period: "30 min",
            }            
        }
    ]
}