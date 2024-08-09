VAR choice = 0

    {
        -choice == 1:  
            -> fanyang  
        -choice == 2:  
            -> guiying 
        -choice == 3:
            -> yijiao
        -choice == 4:
            -> nohandle
    }  
  
== fanyang
    // 返阳对话写在下方
    鬼魂：咦？ 
    金翎（秦广王）：怎么把大好人才给我送走了！
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：好的，我会留意信息。
    ->END

== yijiao
    // 移交对话写在下
    鬼魂：咦？
    金翎（秦广王）：怎么把大好人才给我送走了！
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END