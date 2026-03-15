// _AudioTexture is declared in lil_common_input.hlsl when LIL_FEATURE_AUDIOLINK is defined.
// #if !defined(LIL_FEATURE_AUDIOLINK) && !defined(AUDIOLINK_CGINC_INCLUDED)
// TEXTURE2D_FLOAT(_AudioTexture);
// float4 _AudioTexture_TexelSize;
// #endif  // !defined(LIL_FEATURE_AUDIOLINK) && !defined(AUDIOLINK_CGINC_INCLUDED)

// ProTV texture.
// TEXTURE2D(_Udon_VideoTex);
// float4 _Udon_VideoTex_TexelSize;

// 0: Rendering normally
// 1: Rendering in VR handheld camera
// 2: Rendering in Desktop handheld camera
// 3: Rendering for a screenshot
// float _VRChatCameraMode;

// 0: Rendering normally, not in a mirror
// 1: Rendering in a mirror viewed in VR
// 2: Rendering in a mirror viewed in desktop mode
// float _VRChatMirrorMode;

// World space position of mirror camera (eye independent, "centered" in VR)
// (0,0,0) when not rendering in a mirror.
// float _VRChatMirrorCameraPos;

// World space position of main screen camera.
// float3 _VRChatScreenCameraPos;
// World space rotation (quaternion) of main screen camera.
// float4 _VRChatScreenCameraRot;

// World space position of handheld photo camera (first instance when using Dolly Multicam), (0,0,0) when camera is not active.
// float3 _VRChatPhotoCameraPos;
// World space rotation (quaternion) of photo camera.
// float4 _VRChatPhotoCameraRot;

// The lower 32 bits of the current UTC time in seconds since the Unix epoch.
// Note that this should be treated as an unsigned number and will thus not (yet) overflow in 2038.
// If system time is set to pre-1970 this value is undefined.
// uint _VRChatTimeUTCUnixSeconds;
// Synchronized network time in milliseconds.
// This is the same value as returned by `Networking.GetServerTimeInMilliseconds` in Udon.
// This is technically a signed value, but may be treated as unsigned.
// It should only be used for synchronization and offsets, as the absolute value does not represent any meaningful quantity.
// This value can wrap.
// uint _VRChatTimeNetworkMs;
// bit 0-4: Hour component of the current time of day (UTC).
// bit 5-10: Minute component of the current time of day (UTC).
// bit 11-16: Second component of the current time of day (UTC & Local, shared).
// bit 17-21: Hour component of the current time of day (Local).
// bit 22-27: Minute component of the current time of day (Local).
// bit 28-31: Reserved.
// uint _VRChatTimeEncoded1;
// bit 0-9: Millisecond component of the current time of day (UTC & Local, shared).
// bit 10: Sign bit of timezone offset. 1 if offset is negative.
// bit 11-26: Timezone offset from UTC to Local time in seconds.
// bit 27-31: Reserved.
// uint _VRChatTimeEncoded2;
