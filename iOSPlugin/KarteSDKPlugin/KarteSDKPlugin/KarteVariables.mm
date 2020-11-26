#import <Foundation/Foundation.h>
#import "Util.h"
@import KarteCore;
@import KarteVariables;

extern "C" {
    void UnitySendMessage(const char *obj, const char *method, const char *msg);
    
    NSArray* variablesFromKeys(const char *serializedKeys) {
        NSArray *keys = [Util arrayWithSerializedString:serializedKeys];
        NSMutableArray *variables = [[NSMutableArray alloc] init];
        for (NSString *key in keys) {
            KRTVariable *variable = [KRTVariables variableForKey:key];
            [variables addObject:variable];
        }
        return variables;
    }

    void KRTVariables_fetch() {
        [KRTVariables fetchWithCompletion:nil];
    }

    void KRTVariables_fetchWithCompletionBlock(const char *callbackTarget, const char *callbackId) {
        NSString *callbackIdStr = [NSString stringWithCString:callbackId encoding:NSUTF8StringEncoding];
        const char *copy = strdup(callbackTarget);
        [KRTVariables fetchWithCompletion:^(BOOL isSuccessful) {
            NSString *result = isSuccessful ? @"true" : @"false";
            NSDictionary *dict = @{@"result": result, @"callbackId": callbackIdStr};
            NSData *data = [NSJSONSerialization dataWithJSONObject:dict options:0 error:nil];
            NSString *serializedArguments = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            UnitySendMessage(copy, "VariableCallback", strdup([serializedArguments UTF8String]));
        }];
    }

    void KRTVariables_trackOpen(const char *serializedKeys) {
        NSArray *variables = variablesFromKeys(serializedKeys);
        [KRTTracker trackOpenWithVariables:variables];
    }

    void KRTVariables_trackOpenWithValues(const char *serializedKeys, const char *serializedValues) {
        NSArray *variables = variablesFromKeys(serializedKeys);
        NSDictionary *values = [Util dictionaryWithSerializedString:serializedValues];
        [KRTTracker trackOpenWithVariables:variables values:values];
    }

    void KRTVariables_trackClick(const char *serializedKeys) {
        NSArray *variables = variablesFromKeys(serializedKeys);
        [KRTTracker trackClickWithVariables:variables];
    }

    void KRTVariables_trackClickWithValues(const char *serializedKeys, const char *serializedValues) {
        NSDictionary *values = [Util dictionaryWithSerializedString:serializedValues];
        NSArray *variables = variablesFromKeys(serializedKeys);
        [KRTTracker trackClickWithVariables:variables values:values];
    }

    const char *KRTVariables_string(const char* key) {
        NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
        KRTVariable *variable = [KRTVariables variableForKey:keyStr];
        NSString *str = [variable string];
        RETURN_RETAINED_STR(str)
    }

    long KRTVariables_integer(const char* key, long defaultValue) {
        NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
        KRTVariable *variable = [KRTVariables variableForKey:keyStr];
        long value = [variable integerWithDefaultValue:defaultValue];
        return value;
    }

    double KRTVariables_double(const char* key, double defaultValue) {
        NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
        KRTVariable *variable = [KRTVariables variableForKey:keyStr];
        double value = [variable doubleWithDefaultValue:defaultValue];
        return value;
    }

    int KRTVariables_bool(const char* key, BOOL defaultValue) {
         NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
         KRTVariable *variable = [KRTVariables variableForKey:keyStr];
         int value = [variable boolWithDefaultValue:defaultValue];
         return value;
    }

    const char *KRTVariables_array(const char* key) {
        NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
        KRTVariable *variable = [KRTVariables variableForKey:keyStr];
        NSArray* value = [variable array];
        if (value == nil) {
            return nil;
        }
        NSData *data = [NSJSONSerialization dataWithJSONObject:value options:0 error:nil];
        NSString *serialized = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        RETURN_RETAINED_STR(serialized)
    }

    const char *KRTVariables_object(const char* key) {
        NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
        KRTVariable *variable = [KRTVariables variableForKey:keyStr];
        NSDictionary* value = [variable dictionary];
        if (value == nil) {
            return nil;
        }
        NSData *data = [NSJSONSerialization dataWithJSONObject:value options:0 error:nil];
        NSString *serialized = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        RETURN_RETAINED_STR(serialized)
    }

    int KRTVariables_isDefined(const char* key) {
         NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
         KRTVariable *variable = [KRTVariables variableForKey:keyStr];
        return [variable isDefined];
    }
}
