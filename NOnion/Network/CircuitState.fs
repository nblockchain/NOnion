﻿namespace NOnion.Network

open System.Threading.Tasks

open Org.BouncyCastle.Crypto.Parameters

open NOnion.Crypto
open NOnion.TorHandshakes

//TODO: Implement states like destroyed, truncated, etc...

type TorCircuitNode =
    {
        CryptoState: TorCryptoState
        Window: TorWindow
    }

type CircuitState =
    | Initialized
    | Creating of
        circuitId: uint16 *
        handshakeState: IHandshake *
        completionTask: TaskCompletionSource<uint16>
    | Extending of
        circuitId: uint16 *
        handshakeState: IHandshake *
        currentCircuitNodes: List<TorCircuitNode> *
        completionTask: TaskCompletionSource<uint16>
    | RegisteringAsIntorductionPoint of
        circuitId: uint16 *
        circuitNodes: List<TorCircuitNode> *
        privateKey: Ed25519PrivateKeyParameters *
        publicKey: Ed25519PublicKeyParameters *
        completionTask: TaskCompletionSource<unit>
    | Ready of circuitId: uint16 * circuitNodes: List<TorCircuitNode>
    | ReadyAsIntroductionPoint of
        circuitId: uint16 *
        circuitNodes: List<TorCircuitNode> *
        privateKey: Ed25519PrivateKeyParameters *
        publicKey: Ed25519PublicKeyParameters
    | Destroyed of circuitId: uint16 * reason: byte
    | Truncated of circuitId: uint16 * reason: byte
