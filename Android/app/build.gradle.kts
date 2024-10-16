plugins {
    alias(libs.plugins.android.application)
}

android {
    namespace = "com.example.mylaptop"
    compileSdk = 34

    defaultConfig {
        applicationId = "com.example.mylaptop"
        minSdk = 24
        targetSdk = 34
        versionCode = 1
        versionName = "1.0"

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_1_8
        targetCompatibility = JavaVersion.VERSION_1_8
    }
}

dependencies {

    implementation(libs.appcompat)
    implementation(libs.material)
    implementation(libs.activity)
    implementation(libs.constraintlayout)
    testImplementation(libs.junit)
    androidTestImplementation(libs.ext.junit)
    androidTestImplementation(libs.espresso.core)
    // https://mvnrepository.com/artifact/com.github.bumptech.glide/glide
    implementation(libs.glide)
    annotationProcessor(libs.compiler)

    // https://mvnrepository.com/artifact/com.squareup.retrofit2/retrofit
    implementation(libs.retrofit)
    // https://mvnrepository.com/artifact/com.squareup.retrofit2/converter-gson
    implementation(libs.converter.gson)
// https://mvnrepository.com/artifact/com.squareup.okhttp3/logging-interceptor
    implementation(libs.logging.interceptor)

}