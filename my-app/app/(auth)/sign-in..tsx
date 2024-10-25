import React, { useState } from 'react'
import { Link, router } from 'expo-router'
import { View, Text, ScrollView, Dimensions, Image, SafeAreaView } from 'react-native'
//@ts-ignore
import { images } from '@/constants'
import FormField from '@/components/FormField'
import CustomButton from '@/components/CustomButton'
import { useLoginMutation } from '@/services/accountService'
// import { saveToSecureStore } from '@/utils/secureStore'
// import { setCredentials } from '@/redux/slices/userSlice'
// import { jwtParse } from '@/utils/jwtParser'
// import { IUser } from '@/interfaces/account'
import { useAppDispatch } from '@/redux/store'

const SignIn = () => {
    const [email, setEmail] = React.useState('')
    const [password, setPassword] = React.useState('')

    // const dispatch = useAppDispatch()
    //
    const [login, { isLoading }] = useLoginMutation()

    const submit = async () => {
        // try {
        //     const res = await login({ email, password }).unwrap()
        //
        //     await saveToSecureStore('authToken', res.token)
        //     dispatch(setCredentials({ user: jwtParse(res.token) as IUser, token: res.token }))
        //
        //     router.replace('/pizzas')
        // } catch (error: any) {
        //     console.log(error)
        //     alert(error.data)
        // }
    }

    return (
        <SafeAreaView className="bg-primary h-full">
            <ScrollView>
                <View
                    className="w-full flex justify-center items-center h-full px-4 my-6"
                    style={{
                        minHeight: Dimensions.get('window').height - 100,
                    }}>
                    <View className="flex flex-row items-center justify-center">
                        <Image source={images.pizzaLogo} className=" w-[40px] h-[34px]" resizeMode="contain" />
                        <Text className="mt-2 text-4xl font-pbold font-bold text-secondary">MYPIZZA</Text>
                    </View>

                    <Text className="text-2xl font-semibold text-white mt-10 font-psemibold">Log in to My Pizza</Text>

                    <FormField
                        placeholder="Enter your email"
                        title="Email"
                        value={email}
                        handleChangeText={(e) => setEmail(e)}
                        otherStyles="mt-7"
                        keyboardType="email-address"
                    />

                    <FormField
                        placeholder="Enter your password"
                        title="Password"
                        value={password}
                        handleChangeText={(e) => setPassword(e)}
                        otherStyles="mt-7"
                    />

                    <CustomButton title="Sign In" handlePress={submit} containerStyles="mt-7 w-full" isLoading={isLoading} />

                    <View className="flex justify-center pt-5 flex-row gap-2">
                        <Text className="text-sm text-gray-100 font-pregular">Don't have an account?</Text>
                        <Link href="/sign-up" className="text-sm font-psemibold text-secondary">
                            Signup
                        </Link>
                    </View>
                </View>
            </ScrollView>
        </SafeAreaView>
    )
}

export default SignIn
