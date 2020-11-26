//
//  KartePluginLoader.m
//  KarteSDKPlugin
//
//  Created by kota.fujiwara on 2020/10/22.
//  Copyright © 2020 PLAID, inc. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "KartePluginLoader.h"

@implementation KartePluginLoaderInternal

+ (void)handleLoad
{
}

@end

@implementation KartePluginLoader

+ (void)load
{
    [super load];
    [self handleLoad];
}

@end
