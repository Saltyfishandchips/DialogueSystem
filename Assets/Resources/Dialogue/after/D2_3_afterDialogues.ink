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
    鬼魂：回去再玩两轮！
    金翎（秦广王）：鬼差抢单这段时间很常见。
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：申请该去哪个部门……
    金翎（秦广王）：鬼差抢单这段时间很常见。
    ->END

== yijiao
    // 移交对话写在下
    鬼魂：记得给他们打通灵电话！
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END