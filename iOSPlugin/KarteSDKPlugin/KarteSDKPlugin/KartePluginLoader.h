//
//  KartePluginLoader.h
//  KarteSDKPlugin
//
//  Created by kota.fujiwara on 2020/10/22.
//  Copyright © 2020 PLAID, inc. All rights reserved.
//
#import <Foundation/Foundation.h>

#ifndef KartePluginLoader_h
#define KartePluginLoader_h

@interface KartePluginLoaderInternal : NSObject
+ (void)handleLoad;
@end

@interface KartePluginLoader : KartePluginLoaderInternal
@end


#endif /* KartePluginLoader_h */
