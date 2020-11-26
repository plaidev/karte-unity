#import "Util.h"

@implementation Util
+ (NSDictionary*) dictionaryWithSerializedString: (const char *)serializedValues {
    return (NSDictionary*)[Util deserializeString: serializedValues];
}

+ (NSArray*) arrayWithSerializedString: (const char *)serializedValues {
    return (NSArray*)[Util deserializeString: serializedValues];
}

+ (id) deserializeString: (const char *)serializedValues {
    NSString *jsonStr = [NSString stringWithCString:serializedValues encoding:NSUTF8StringEncoding];
    NSData *values = [jsonStr dataUsingEncoding:NSUTF8StringEncoding];
    return [NSJSONSerialization JSONObjectWithData:values options:0 error:nil];
}

@end
