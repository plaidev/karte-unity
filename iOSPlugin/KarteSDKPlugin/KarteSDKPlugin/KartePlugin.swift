//
//  Dummy.swift
//  KarteSDKPlugin
//
//  Created by kota.fujiwara on 2020/09/10.
//  Copyright © 2020 PLAID, inc. All rights reserved.
//

import Foundation
import KarteCore

class KartePlugin: NSObject, Library {
    static var name: String {
        "unity"
    }
    
    static var version: String {
        "1.0.0"
    }
    
    static var isPublic: Bool {
        true
    }
    
    static func configure(app: KarteApp) {
    }
    
    static func unconfigure(app: KarteApp) {
    }
}

extension KartePluginLoader {
    open override class func handleLoad() {
        KarteApp.register(library: KartePlugin.self)
    }
}
