//
//  KarteTrackerPlugin.h
//  Unity-iPhone
//
//  Created by kota.fujiwara on 2019/03/22.
//

#ifndef KarteTrackerPlugin_h
#define KarteTrackerPlugin_h
#define RETURN_RETAINED_STR(nsstring) if(!nsstring){ return nil; } else { return strdup([nsstring UTF8String]); }
#endif /* KarteTrackerPlugin_h */

#import <Foundation/Foundation.h>

@interface Util: NSObject {}
+ (NSDictionary*) dictionaryWithSerializedString: (const char *)serializedValues;
+ (NSArray*) arrayWithSerializedString: (const char *)serializedValues;
@end
