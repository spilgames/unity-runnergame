extern "C"
{
    char* _getGroupUserID()
    
    {

        
        NSUserDefaults *myDefaults = [[NSUserDefaults alloc]
                                      initWithSuiteName:@"group.com.spilgames"];
        NSString *userId = [myDefaults objectForKey:@"com.spilgames.userid"];
        
        if(userId == NULL){
            
            NSString *UUID = [[NSUUID UUID] UUIDString];
            [myDefaults setObject:(NSString*)UUID forKey:@"com.spilgames.userid"];
            [myDefaults synchronize];
        }
        
        userId = [myDefaults objectForKey:@"com.spilgames.userid"];
        
        const char* string = [userId UTF8String];
        
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        
        return res;
  
    }
    
}

