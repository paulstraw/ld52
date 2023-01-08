import React from "react"
import { Unity, useUnityContext } from "react-unity-webgl"

import './App.css';

function App() {
  const { isLoaded, loadingProgression, unityProvider } = useUnityContext({
    loaderUrl: "unity-build/Build/unity-build.loader.js",
    dataUrl: "unity-build/Build/unity-build.data",
    frameworkUrl: "unity-build/Build/unity-build.framework.js",
    codeUrl: "unity-build/Build/unity-build.wasm",
    productName: "Bad Apples",
    companyName: "Straw Dynamics",
  })

  const loadingPercentage = Math.round(loadingProgression * 100)

  return (
    <>
      {!isLoaded && <p className="loading-text">Loading… {loadingPercentage}%</p>}
      <Unity unityProvider={unityProvider} style={{width: '100vw', height: '100vh'}} />
    </>
  )
}

export default App
