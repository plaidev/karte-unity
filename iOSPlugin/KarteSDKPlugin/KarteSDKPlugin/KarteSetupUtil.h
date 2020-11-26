//
//  KarteTrackerSetupUtil.h
//  KarteTrackerUnityPlugin
//
//  Created by kota.fujiwara on 2019/08/01.
//  Copyright © 2019 PLAID, inc. All rights reserved.
//

#import <Foundation/Foundation.h>
@import KarteCore;

NS_ASSUME_NONNULL_BEGIN

@interface KarteSetupUtil : NSObject
+ (void)setupWithConfigFile;
+ (KRTConfiguration *) configWithDictionary: (NSDictionary *)dict;
@end

NS_ASSUME_NONNULL_END
