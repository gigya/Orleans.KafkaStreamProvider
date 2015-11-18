#if !EXCLUDE_CODEGEN
#pragma warning disable 162
#pragma warning disable 219
#pragma warning disable 414
#pragma warning disable 649
#pragma warning disable 693
#pragma warning disable 1591
#pragma warning disable 1998
[assembly: global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8")]
[assembly: global::Orleans.CodeGeneration.OrleansCodeGenerationTargetAttribute("TestGrainInterfaces, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null")]
namespace TestGrainInterfaces
{
    using global::Orleans.Async;
    using global::Orleans;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.IBatchProducerGrain))]
    internal class OrleansCodeGenBatchProducerGrainReference : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.IBatchProducerGrain
    {
        protected @OrleansCodeGenBatchProducerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenBatchProducerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1056993933;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.IBatchProducerGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1056993933;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1056993933:
                    switch (@methodId)
                    {
                        case 266083571:
                            return "BecomeProducer";
                        case -1391159795:
                            return "StartPeriodicBatchProducing";
                        case 1830475170:
                            return "StopPeriodicBatchProducing";
                        case -970329735:
                            return "GetNumberProduced";
                        case 1732143298:
                            return "ClearNumberProduced";
                        case 62587811:
                            return "Produce";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1056993933 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @BecomeProducer(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::System.Object>(266083571, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task @StartPeriodicBatchProducing(global::System.Int32 @batchSize)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1391159795, new global::System.Object[]{@batchSize});
        }

        public global::System.Threading.Tasks.Task @StopPeriodicBatchProducing()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1830475170, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberProduced()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-970329735, null);
        }

        public global::System.Threading.Tasks.Task @ClearNumberProduced()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1732143298, null);
        }

        public global::System.Threading.Tasks.Task @Produce(global::System.Int32 @batchSize)
        {
            return base.@InvokeMethodAsync<global::System.Object>(62587811, new global::System.Object[]{@batchSize});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.IBatchProducerGrain", 1056993933, typeof (global::TestGrainInterfaces.IBatchProducerGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenBatchProducerGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1056993933:
                        switch (@methodId)
                        {
                            case 266083571:
                                return ((global::TestGrainInterfaces.IBatchProducerGrain)@grain).@BecomeProducer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case -1391159795:
                                return ((global::TestGrainInterfaces.IBatchProducerGrain)@grain).@StartPeriodicBatchProducing((global::System.Int32)@arguments[0]).@Box();
                            case 1830475170:
                                return ((global::TestGrainInterfaces.IBatchProducerGrain)@grain).@StopPeriodicBatchProducing().@Box();
                            case -970329735:
                                return ((global::TestGrainInterfaces.IBatchProducerGrain)@grain).@GetNumberProduced().@Box();
                            case 1732143298:
                                return ((global::TestGrainInterfaces.IBatchProducerGrain)@grain).@ClearNumberProduced().@Box();
                            case 62587811:
                                return ((global::TestGrainInterfaces.IBatchProducerGrain)@grain).@Produce((global::System.Int32)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1056993933 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1056993933;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.ICustomEventConsumerGrain<>))]
    internal class OrleansCodeGenCustomEventConsumerGrainReference<T> : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.ICustomEventConsumerGrain<T>
    {
        protected @OrleansCodeGenCustomEventConsumerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenCustomEventConsumerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1173705989;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.ICustomEventConsumerGrain<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1173705989 || @interfaceId == -1573917447;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1573917447:
                    switch (@methodId)
                    {
                        case 210218485:
                            return "BecomeConsumer";
                        case 1151053849:
                            return "StopConsuming";
                        case 2130169286:
                            return "GetNumberConsumed";
                        case -1340838591:
                            return "GetLastConsumedItem";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1573917447 + ",methodId=" + @methodId);
                    }

                case 1173705989:
                    switch (@methodId)
                    {
                        case 210218485:
                            return "BecomeConsumer";
                        case 1151053849:
                            return "StopConsuming";
                        case 2130169286:
                            return "GetNumberConsumed";
                        case -1340838591:
                            return "GetLastConsumedItem";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1173705989 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @BecomeConsumer(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::System.Object>(210218485, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task @StopConsuming()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1151053849, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberConsumed()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(2130169286, null);
        }

        public global::System.Threading.Tasks.Task<T> @GetLastConsumedItem()
        {
            return base.@InvokeMethodAsync<T>(-1340838591, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.ICustomEventConsumerGrain<T>", 1173705989, typeof (global::TestGrainInterfaces.ICustomEventConsumerGrain<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenCustomEventConsumerGrainMethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1573917447:
                        switch (@methodId)
                        {
                            case 210218485:
                                return ((global::TestGrainInterfaces.ICustomEventConsumerGrain<T>)@grain).@BecomeConsumer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 1151053849:
                                return ((global::TestGrainInterfaces.ICustomEventConsumerGrain<T>)@grain).@StopConsuming().@Box();
                            case 2130169286:
                                return ((global::TestGrainInterfaces.ICustomEventConsumerGrain<T>)@grain).@GetNumberConsumed().@Box();
                            case -1340838591:
                                return ((global::TestGrainInterfaces.ICustomEventConsumerGrain<T>)@grain).@GetLastConsumedItem().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1573917447 + ",methodId=" + @methodId);
                        }

                    case 1173705989:
                        switch (@methodId)
                        {
                            case 210218485:
                                return ((global::TestGrainInterfaces.ICustomEventConsumerGrain<T>)@grain).@BecomeConsumer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 1151053849:
                                return ((global::TestGrainInterfaces.ICustomEventConsumerGrain<T>)@grain).@StopConsuming().@Box();
                            case 2130169286:
                                return ((global::TestGrainInterfaces.ICustomEventConsumerGrain<T>)@grain).@GetNumberConsumed().@Box();
                            case -1340838591:
                                return ((global::TestGrainInterfaces.ICustomEventConsumerGrain<T>)@grain).@GetLastConsumedItem().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1173705989 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1173705989;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.ICustomEventProducerGrain<>))]
    internal class OrleansCodeGenCustomEventProducerGrainReference<T> : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.ICustomEventProducerGrain<T>
    {
        protected @OrleansCodeGenCustomEventProducerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenCustomEventProducerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 979969149;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.ICustomEventProducerGrain<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 979969149 || @interfaceId == -589552067;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -589552067:
                    switch (@methodId)
                    {
                        case 266083571:
                            return "BecomeProducer";
                        case 521990652:
                            return "StartPeriodicBatchProducing";
                        case 1830475170:
                            return "StopPeriodicBatchProducing";
                        case -970329735:
                            return "GetNumberProduced";
                        case 1732143298:
                            return "ClearNumberProduced";
                        case -1093083721:
                            return "Produce";
                        case -803071648:
                            return "GetLastProducedItem";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -589552067 + ",methodId=" + @methodId);
                    }

                case 979969149:
                    switch (@methodId)
                    {
                        case 266083571:
                            return "BecomeProducer";
                        case 521990652:
                            return "StartPeriodicBatchProducing";
                        case 1830475170:
                            return "StopPeriodicBatchProducing";
                        case -970329735:
                            return "GetNumberProduced";
                        case 1732143298:
                            return "ClearNumberProduced";
                        case -1093083721:
                            return "Produce";
                        case -803071648:
                            return "GetLastProducedItem";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 979969149 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @BecomeProducer(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::System.Object>(266083571, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task @StartPeriodicBatchProducing(global::System.Int32 @batchSize, T @itemToProduce)
        {
            return base.@InvokeMethodAsync<global::System.Object>(521990652, new global::System.Object[]{@batchSize, @itemToProduce});
        }

        public global::System.Threading.Tasks.Task @StopPeriodicBatchProducing()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1830475170, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberProduced()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-970329735, null);
        }

        public global::System.Threading.Tasks.Task @ClearNumberProduced()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1732143298, null);
        }

        public global::System.Threading.Tasks.Task @Produce(global::System.Int32 @batchSize, T @valueToProduce)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1093083721, new global::System.Object[]{@batchSize, @valueToProduce});
        }

        public global::System.Threading.Tasks.Task<T> @GetLastProducedItem()
        {
            return base.@InvokeMethodAsync<T>(-803071648, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.ICustomEventProducerGrain<T>", 979969149, typeof (global::TestGrainInterfaces.ICustomEventProducerGrain<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenCustomEventProducerGrainMethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -589552067:
                        switch (@methodId)
                        {
                            case 266083571:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@BecomeProducer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 521990652:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@StartPeriodicBatchProducing((global::System.Int32)@arguments[0], (T)@arguments[1]).@Box();
                            case 1830475170:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@StopPeriodicBatchProducing().@Box();
                            case -970329735:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@GetNumberProduced().@Box();
                            case 1732143298:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@ClearNumberProduced().@Box();
                            case -1093083721:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@Produce((global::System.Int32)@arguments[0], (T)@arguments[1]).@Box();
                            case -803071648:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@GetLastProducedItem().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -589552067 + ",methodId=" + @methodId);
                        }

                    case 979969149:
                        switch (@methodId)
                        {
                            case 266083571:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@BecomeProducer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 521990652:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@StartPeriodicBatchProducing((global::System.Int32)@arguments[0], (T)@arguments[1]).@Box();
                            case 1830475170:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@StopPeriodicBatchProducing().@Box();
                            case -970329735:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@GetNumberProduced().@Box();
                            case 1732143298:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@ClearNumberProduced().@Box();
                            case -1093083721:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@Produce((global::System.Int32)@arguments[0], (T)@arguments[1]).@Box();
                            case -803071648:
                                return ((global::TestGrainInterfaces.ICustomEventProducerGrain<T>)@grain).@GetLastProducedItem().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 979969149 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 979969149;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.IMultipleProducingProducerGrain))]
    internal class OrleansCodeGenMultipleProducingProducerGrainReference : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.IMultipleProducingProducerGrain
    {
        protected @OrleansCodeGenMultipleProducingProducerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenMultipleProducingProducerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 271298961;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.IMultipleProducingProducerGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 271298961;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 271298961:
                    switch (@methodId)
                    {
                        case 266083571:
                            return "BecomeProducer";
                        case 1365048832:
                            return "StartPeriodicProducing";
                        case -282951924:
                            return "StopPeriodicProducing";
                        case -970329735:
                            return "GetNumberProduced";
                        case 1732143298:
                            return "ClearNumberProduced";
                        case -871495156:
                            return "Produce";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 271298961 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::Orleans.Streams.IStreamIdentity> @BecomeProducer(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::Orleans.Streams.IStreamIdentity>(266083571, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task @StartPeriodicProducing(global::Orleans.Streams.IStreamIdentity @identity)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1365048832, new global::System.Object[]{@identity});
        }

        public global::System.Threading.Tasks.Task @StopPeriodicProducing(global::Orleans.Streams.IStreamIdentity @identity)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-282951924, new global::System.Object[]{@identity});
        }

        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.Dictionary<global::Orleans.Streams.IStreamIdentity, global::System.Int32>> @GetNumberProduced()
        {
            return base.@InvokeMethodAsync<global::System.Collections.Generic.Dictionary<global::Orleans.Streams.IStreamIdentity, global::System.Int32>>(-970329735, null);
        }

        public global::System.Threading.Tasks.Task @ClearNumberProduced()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1732143298, null);
        }

        public global::System.Threading.Tasks.Task @Produce(global::Orleans.Streams.IStreamIdentity @identifier)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-871495156, new global::System.Object[]{@identifier});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.IMultipleProducingProducerGrain", 271298961, typeof (global::TestGrainInterfaces.IMultipleProducingProducerGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenMultipleProducingProducerGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 271298961:
                        switch (@methodId)
                        {
                            case 266083571:
                                return ((global::TestGrainInterfaces.IMultipleProducingProducerGrain)@grain).@BecomeProducer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 1365048832:
                                return ((global::TestGrainInterfaces.IMultipleProducingProducerGrain)@grain).@StartPeriodicProducing((global::Orleans.Streams.IStreamIdentity)@arguments[0]).@Box();
                            case -282951924:
                                return ((global::TestGrainInterfaces.IMultipleProducingProducerGrain)@grain).@StopPeriodicProducing((global::Orleans.Streams.IStreamIdentity)@arguments[0]).@Box();
                            case -970329735:
                                return ((global::TestGrainInterfaces.IMultipleProducingProducerGrain)@grain).@GetNumberProduced().@Box();
                            case 1732143298:
                                return ((global::TestGrainInterfaces.IMultipleProducingProducerGrain)@grain).@ClearNumberProduced().@Box();
                            case -871495156:
                                return ((global::TestGrainInterfaces.IMultipleProducingProducerGrain)@grain).@Produce((global::Orleans.Streams.IStreamIdentity)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 271298961 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 271298961;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.IDoSomethingWithMoreGrain))]
    internal class OrleansCodeGenDoSomethingWithMoreGrainReference : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.IDoSomethingWithMoreGrain
    {
        protected @OrleansCodeGenDoSomethingWithMoreGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenDoSomethingWithMoreGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1194674781;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.IDoSomethingWithMoreGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1194674781;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1194674781:
                    switch (@methodId)
                    {
                        case -1313450312:
                            return "DoThat";
                        case 638886962:
                            return "SetB";
                        case -1953666708:
                            return "IncrementB";
                        case -375877609:
                            return "GetB";
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1194674781 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoThat()
        {
            return base.@InvokeMethodAsync<global::System.String>(-1313450312, null);
        }

        public global::System.Threading.Tasks.Task @SetB(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(638886962, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @IncrementB()
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1953666708, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetB()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-375877609, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoIt()
        {
            return base.@InvokeMethodAsync<global::System.String>(-2085989032, null);
        }

        public global::System.Threading.Tasks.Task @SetA(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(2129932222, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @IncrementA()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1017190206, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetA()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-411561932, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.IDoSomethingWithMoreGrain", 1194674781, typeof (global::TestGrainInterfaces.IDoSomethingWithMoreGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenDoSomethingWithMoreGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1194674781:
                        switch (@methodId)
                        {
                            case -1313450312:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreGrain)@grain).@DoThat().@Box();
                            case 638886962:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreGrain)@grain).@SetB((global::System.Int32)@arguments[0]).@Box();
                            case -1953666708:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreGrain)@grain).@IncrementB().@Box();
                            case -375877609:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreGrain)@grain).@GetB().@Box();
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1194674781 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1194674781;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.IDoSomethingEmptyGrain))]
    internal class OrleansCodeGenDoSomethingEmptyGrainReference : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.IDoSomethingEmptyGrain
    {
        protected @OrleansCodeGenDoSomethingEmptyGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenDoSomethingEmptyGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -391075291;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.IDoSomethingEmptyGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -391075291;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -391075291:
                    switch (@methodId)
                    {
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -391075291 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoIt()
        {
            return base.@InvokeMethodAsync<global::System.String>(-2085989032, null);
        }

        public global::System.Threading.Tasks.Task @SetA(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(2129932222, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @IncrementA()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1017190206, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetA()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-411561932, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.IDoSomethingEmptyGrain", -391075291, typeof (global::TestGrainInterfaces.IDoSomethingEmptyGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenDoSomethingEmptyGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -391075291:
                        switch (@methodId)
                        {
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -391075291 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -391075291;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain))]
    internal class OrleansCodeGenDoSomethingEmptyWithMoreGrainReference : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain
    {
        protected @OrleansCodeGenDoSomethingEmptyWithMoreGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenDoSomethingEmptyWithMoreGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 962951394;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 962951394 || @interfaceId == -391075291;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 962951394:
                    switch (@methodId)
                    {
                        case -881238934:
                            return "DoMore";
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 962951394 + ",methodId=" + @methodId);
                    }

                case -391075291:
                    switch (@methodId)
                    {
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -391075291 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoMore()
        {
            return base.@InvokeMethodAsync<global::System.String>(-881238934, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoIt()
        {
            return base.@InvokeMethodAsync<global::System.String>(-2085989032, null);
        }

        public global::System.Threading.Tasks.Task @SetA(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(2129932222, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @IncrementA()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1017190206, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetA()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-411561932, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain", 962951394, typeof (global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenDoSomethingEmptyWithMoreGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 962951394:
                        switch (@methodId)
                        {
                            case -881238934:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)@grain).@DoMore().@Box();
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 962951394 + ",methodId=" + @methodId);
                        }

                    case -391075291:
                        switch (@methodId)
                        {
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingEmptyWithMoreGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -391075291 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 962951394;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain))]
    internal class OrleansCodeGenDoSomethingWithMoreEmptyGrainReference : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain
    {
        protected @OrleansCodeGenDoSomethingWithMoreEmptyGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenDoSomethingWithMoreEmptyGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 2141373979;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 2141373979 || @interfaceId == 962951394 || @interfaceId == -391075291;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 2141373979:
                    switch (@methodId)
                    {
                        case -881238934:
                            return "DoMore";
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 2141373979 + ",methodId=" + @methodId);
                    }

                case 962951394:
                    switch (@methodId)
                    {
                        case -881238934:
                            return "DoMore";
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 962951394 + ",methodId=" + @methodId);
                    }

                case -391075291:
                    switch (@methodId)
                    {
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -391075291 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoMore()
        {
            return base.@InvokeMethodAsync<global::System.String>(-881238934, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoIt()
        {
            return base.@InvokeMethodAsync<global::System.String>(-2085989032, null);
        }

        public global::System.Threading.Tasks.Task @SetA(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(2129932222, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @IncrementA()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1017190206, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetA()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-411561932, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain", 2141373979, typeof (global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenDoSomethingWithMoreEmptyGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 2141373979:
                        switch (@methodId)
                        {
                            case -881238934:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@DoMore().@Box();
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 2141373979 + ",methodId=" + @methodId);
                        }

                    case 962951394:
                        switch (@methodId)
                        {
                            case -881238934:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@DoMore().@Box();
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 962951394 + ",methodId=" + @methodId);
                        }

                    case -391075291:
                        switch (@methodId)
                        {
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingWithMoreEmptyGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -391075291 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 2141373979;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.IDoSomethingCombinedGrain))]
    internal class OrleansCodeGenDoSomethingCombinedGrainReference : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.IDoSomethingCombinedGrain
    {
        protected @OrleansCodeGenDoSomethingCombinedGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenDoSomethingCombinedGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -514373012;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.IDoSomethingCombinedGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -514373012 || @interfaceId == 1194674781 || @interfaceId == 2141373979 || @interfaceId == 962951394 || @interfaceId == -391075291;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -514373012:
                    switch (@methodId)
                    {
                        case -1229111471:
                            return "SetC";
                        case -2019798947:
                            return "IncrementC";
                        case -186959875:
                            return "GetC";
                        case -1313450312:
                            return "DoThat";
                        case 638886962:
                            return "SetB";
                        case -1953666708:
                            return "IncrementB";
                        case -375877609:
                            return "GetB";
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        case -881238934:
                            return "DoMore";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -514373012 + ",methodId=" + @methodId);
                    }

                case 1194674781:
                    switch (@methodId)
                    {
                        case -1313450312:
                            return "DoThat";
                        case 638886962:
                            return "SetB";
                        case -1953666708:
                            return "IncrementB";
                        case -375877609:
                            return "GetB";
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1194674781 + ",methodId=" + @methodId);
                    }

                case 2141373979:
                    switch (@methodId)
                    {
                        case -881238934:
                            return "DoMore";
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 2141373979 + ",methodId=" + @methodId);
                    }

                case 962951394:
                    switch (@methodId)
                    {
                        case -881238934:
                            return "DoMore";
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 962951394 + ",methodId=" + @methodId);
                    }

                case -391075291:
                    switch (@methodId)
                    {
                        case -2085989032:
                            return "DoIt";
                        case 2129932222:
                            return "SetA";
                        case 1017190206:
                            return "IncrementA";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -391075291 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetC(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1229111471, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @IncrementC()
        {
            return base.@InvokeMethodAsync<global::System.Object>(-2019798947, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetC()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-186959875, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoThat()
        {
            return base.@InvokeMethodAsync<global::System.String>(-1313450312, null);
        }

        public global::System.Threading.Tasks.Task @SetB(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(638886962, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @IncrementB()
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1953666708, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetB()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-375877609, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoIt()
        {
            return base.@InvokeMethodAsync<global::System.String>(-2085989032, null);
        }

        public global::System.Threading.Tasks.Task @SetA(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(2129932222, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @IncrementA()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1017190206, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetA()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-411561932, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @DoMore()
        {
            return base.@InvokeMethodAsync<global::System.String>(-881238934, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.IDoSomethingCombinedGrain", -514373012, typeof (global::TestGrainInterfaces.IDoSomethingCombinedGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenDoSomethingCombinedGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -514373012:
                        switch (@methodId)
                        {
                            case -1229111471:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@SetC((global::System.Int32)@arguments[0]).@Box();
                            case -2019798947:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@IncrementC().@Box();
                            case -186959875:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@GetC().@Box();
                            case -1313450312:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoThat().@Box();
                            case 638886962:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@SetB((global::System.Int32)@arguments[0]).@Box();
                            case -1953666708:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@IncrementB().@Box();
                            case -375877609:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@GetB().@Box();
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@GetA().@Box();
                            case -881238934:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoMore().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -514373012 + ",methodId=" + @methodId);
                        }

                    case 1194674781:
                        switch (@methodId)
                        {
                            case -1313450312:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoThat().@Box();
                            case 638886962:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@SetB((global::System.Int32)@arguments[0]).@Box();
                            case -1953666708:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@IncrementB().@Box();
                            case -375877609:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@GetB().@Box();
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1194674781 + ",methodId=" + @methodId);
                        }

                    case 2141373979:
                        switch (@methodId)
                        {
                            case -881238934:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoMore().@Box();
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 2141373979 + ",methodId=" + @methodId);
                        }

                    case 962951394:
                        switch (@methodId)
                        {
                            case -881238934:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoMore().@Box();
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 962951394 + ",methodId=" + @methodId);
                        }

                    case -391075291:
                        switch (@methodId)
                        {
                            case -2085989032:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@DoIt().@Box();
                            case 2129932222:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@IncrementA().@Box();
                            case -411561932:
                                return ((global::TestGrainInterfaces.IDoSomethingCombinedGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -391075291 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -514373012;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::TestGrainInterfaces.ITimedConsumerGrain))]
    internal class OrleansCodeGenTimedConsumerGrainReference : global::Orleans.Runtime.GrainReference, global::TestGrainInterfaces.ITimedConsumerGrain
    {
        protected @OrleansCodeGenTimedConsumerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenTimedConsumerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1522249479;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::TestGrainInterfaces.ITimedConsumerGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1522249479;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1522249479:
                    switch (@methodId)
                    {
                        case -286663252:
                            return "BecomeConsumer";
                        case 1151053849:
                            return "StopConsuming";
                        case -1852203042:
                            return "GetReceivedTokens";
                        case 2130169286:
                            return "GetNumberConsumed";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1522249479 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>> @BecomeConsumer(global::System.Guid @streamId, global::System.String @streamNamespace, global::Orleans.Streams.StreamSequenceToken @token, global::System.TimeSpan @timeToConsume, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>>(-286663252, new global::System.Object[]{@streamId, @streamNamespace, @token, @timeToConsume, @providerToUse});
        }

        public global::System.Threading.Tasks.Task @StopConsuming()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1151053849, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.Dictionary<global::System.Int32, global::Orleans.Streams.StreamSequenceToken>> @GetReceivedTokens()
        {
            return base.@InvokeMethodAsync<global::System.Collections.Generic.Dictionary<global::System.Int32, global::Orleans.Streams.StreamSequenceToken>>(-1852203042, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberConsumed()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(2130169286, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::TestGrainInterfaces.ITimedConsumerGrain", 1522249479, typeof (global::TestGrainInterfaces.ITimedConsumerGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenTimedConsumerGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1522249479:
                        switch (@methodId)
                        {
                            case -286663252:
                                return ((global::TestGrainInterfaces.ITimedConsumerGrain)@grain).@BecomeConsumer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::Orleans.Streams.StreamSequenceToken)@arguments[2], (global::System.TimeSpan)@arguments[3], (global::System.String)@arguments[4]).@Box();
                            case 1151053849:
                                return ((global::TestGrainInterfaces.ITimedConsumerGrain)@grain).@StopConsuming().@Box();
                            case -1852203042:
                                return ((global::TestGrainInterfaces.ITimedConsumerGrain)@grain).@GetReceivedTokens().@Box();
                            case 2130169286:
                                return ((global::TestGrainInterfaces.ITimedConsumerGrain)@grain).@GetNumberConsumed().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1522249479 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1522249479;
            }
        }
    }
}

namespace UnitTests.GrainInterfaces
{
    using global::Orleans.Async;
    using global::Orleans;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IFaultableConsumerGrain))]
    internal class OrleansCodeGenFaultableConsumerGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IFaultableConsumerGrain
    {
        protected @OrleansCodeGenFaultableConsumerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenFaultableConsumerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1176703236;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IFaultableConsumerGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1176703236;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1176703236:
                    switch (@methodId)
                    {
                        case 210218485:
                            return "BecomeConsumer";
                        case 1943784544:
                            return "SetFailPeriod";
                        case 1151053849:
                            return "StopConsuming";
                        case 2130169286:
                            return "GetNumberConsumed";
                        case -1999330239:
                            return "GetNumberFailed";
                        case 805055061:
                            return "GetErrorCount";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1176703236 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @BecomeConsumer(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::System.Object>(210218485, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task @SetFailPeriod(global::System.TimeSpan @failPeriod)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1943784544, new global::System.Object[]{@failPeriod});
        }

        public global::System.Threading.Tasks.Task @StopConsuming()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1151053849, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberConsumed()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(2130169286, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberFailed()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-1999330239, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetErrorCount()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(805055061, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IFaultableConsumerGrain", -1176703236, typeof (global::UnitTests.GrainInterfaces.IFaultableConsumerGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenFaultableConsumerGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1176703236:
                        switch (@methodId)
                        {
                            case 210218485:
                                return ((global::UnitTests.GrainInterfaces.IFaultableConsumerGrain)@grain).@BecomeConsumer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 1943784544:
                                return ((global::UnitTests.GrainInterfaces.IFaultableConsumerGrain)@grain).@SetFailPeriod((global::System.TimeSpan)@arguments[0]).@Box();
                            case 1151053849:
                                return ((global::UnitTests.GrainInterfaces.IFaultableConsumerGrain)@grain).@StopConsuming().@Box();
                            case 2130169286:
                                return ((global::UnitTests.GrainInterfaces.IFaultableConsumerGrain)@grain).@GetNumberConsumed().@Box();
                            case -1999330239:
                                return ((global::UnitTests.GrainInterfaces.IFaultableConsumerGrain)@grain).@GetNumberFailed().@Box();
                            case 805055061:
                                return ((global::UnitTests.GrainInterfaces.IFaultableConsumerGrain)@grain).@GetErrorCount().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1176703236 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1176703236;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericGrain<, >))]
    internal class OrleansCodeGenGenericGrainReference<T, U> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericGrain<T, U>
    {
        protected @OrleansCodeGenGenericGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1634994201;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericGrain<T,U>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1634994201 || @interfaceId == 199374749;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 199374749:
                    switch (@methodId)
                    {
                        case 615178881:
                            return "SetT";
                        case -1220533591:
                            return "MapT2U";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 199374749 + ",methodId=" + @methodId);
                    }

                case 1634994201:
                    switch (@methodId)
                    {
                        case 615178881:
                            return "SetT";
                        case -1220533591:
                            return "MapT2U";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1634994201 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetT(T @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(615178881, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task<U> @MapT2U()
        {
            return base.@InvokeMethodAsync<U>(-1220533591, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericGrain<T,U>", 1634994201, typeof (global::UnitTests.GrainInterfaces.IGenericGrain<, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericGrainMethodInvoker<T, U> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 199374749:
                        switch (@methodId)
                        {
                            case 615178881:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrain<T, U>)@grain).@SetT((T)@arguments[0]).@Box();
                            case -1220533591:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrain<T, U>)@grain).@MapT2U().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 199374749 + ",methodId=" + @methodId);
                        }

                    case 1634994201:
                        switch (@methodId)
                        {
                            case 615178881:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrain<T, U>)@grain).@SetT((T)@arguments[0]).@Box();
                            case -1220533591:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrain<T, U>)@grain).@MapT2U().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1634994201 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1634994201;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<>))]
    internal class OrleansCodeGenSimpleGenericGrain1Reference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>
    {
        protected @OrleansCodeGenSimpleGenericGrain1Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSimpleGenericGrain1Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1260283565;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1260283565 || @interfaceId == -140326326;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -140326326:
                    switch (@methodId)
                    {
                        case -411561932:
                            return "GetA";
                        case 1039727631:
                            return "GetAxB";
                        case 923307441:
                            return "GetAxB";
                        case -365341286:
                            return "SetA";
                        case -1084120049:
                            return "SetB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -140326326 + ",methodId=" + @methodId);
                    }

                case 1260283565:
                    switch (@methodId)
                    {
                        case -411561932:
                            return "GetA";
                        case 1039727631:
                            return "GetAxB";
                        case 923307441:
                            return "GetAxB";
                        case -365341286:
                            return "SetA";
                        case -1084120049:
                            return "SetB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1260283565 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<T> @GetA()
        {
            return base.@InvokeMethodAsync<T>(-411561932, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB()
        {
            return base.@InvokeMethodAsync<global::System.String>(1039727631, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB(T @a, T @b)
        {
            return base.@InvokeMethodAsync<global::System.String>(923307441, new global::System.Object[]{@a, @b});
        }

        public global::System.Threading.Tasks.Task @SetA(T @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-365341286, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @SetB(T @b)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1084120049, new global::System.Object[]{@b});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>", 1260283565, typeof (global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSimpleGenericGrain1MethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -140326326:
                        switch (@methodId)
                        {
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@GetA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@GetAxB().@Box();
                            case 923307441:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@GetAxB((T)@arguments[0], (T)@arguments[1]).@Box();
                            case -365341286:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@SetA((T)@arguments[0]).@Box();
                            case -1084120049:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@SetB((T)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -140326326 + ",methodId=" + @methodId);
                        }

                    case 1260283565:
                        switch (@methodId)
                        {
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@GetA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@GetAxB().@Box();
                            case 923307441:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@GetAxB((T)@arguments[0], (T)@arguments[1]).@Box();
                            case -365341286:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@SetA((T)@arguments[0]).@Box();
                            case -1084120049:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain1<T>)@grain).@SetB((T)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1260283565 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1260283565;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<>))]
    internal class OrleansCodeGenSimpleGenericGrainUReference<U> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>
    {
        protected @OrleansCodeGenSimpleGenericGrainUReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSimpleGenericGrainUReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1572190367;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1572190367 || @interfaceId == -841198336;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -841198336:
                    switch (@methodId)
                    {
                        case -411561932:
                            return "GetA";
                        case 1039727631:
                            return "GetAxB";
                        case -331959529:
                            return "GetAxB";
                        case 1405013802:
                            return "SetA";
                        case 1046971231:
                            return "SetB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -841198336 + ",methodId=" + @methodId);
                    }

                case -1572190367:
                    switch (@methodId)
                    {
                        case -411561932:
                            return "GetA";
                        case 1039727631:
                            return "GetAxB";
                        case -331959529:
                            return "GetAxB";
                        case 1405013802:
                            return "SetA";
                        case 1046971231:
                            return "SetB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1572190367 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<U> @GetA()
        {
            return base.@InvokeMethodAsync<U>(-411561932, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB()
        {
            return base.@InvokeMethodAsync<global::System.String>(1039727631, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB(U @a, U @b)
        {
            return base.@InvokeMethodAsync<global::System.String>(-331959529, new global::System.Object[]{@a, @b});
        }

        public global::System.Threading.Tasks.Task @SetA(U @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1405013802, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @SetB(U @b)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1046971231, new global::System.Object[]{@b});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>", -1572190367, typeof (global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSimpleGenericGrainUMethodInvoker<U> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -841198336:
                        switch (@methodId)
                        {
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@GetA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@GetAxB().@Box();
                            case -331959529:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@GetAxB((U)@arguments[0], (U)@arguments[1]).@Box();
                            case 1405013802:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@SetA((U)@arguments[0]).@Box();
                            case 1046971231:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@SetB((U)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -841198336 + ",methodId=" + @methodId);
                        }

                    case -1572190367:
                        switch (@methodId)
                        {
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@GetA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@GetAxB().@Box();
                            case -331959529:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@GetAxB((U)@arguments[0], (U)@arguments[1]).@Box();
                            case 1405013802:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@SetA((U)@arguments[0]).@Box();
                            case 1046971231:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrainU<U>)@grain).@SetB((U)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1572190367 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1572190367;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<, >))]
    internal class OrleansCodeGenSimpleGenericGrain2Reference<T, U> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>
    {
        protected @OrleansCodeGenSimpleGenericGrain2Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSimpleGenericGrain2Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -684597085;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T,U>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -684597085 || @interfaceId == -1571726004;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1571726004:
                    switch (@methodId)
                    {
                        case -411561932:
                            return "GetA";
                        case 1039727631:
                            return "GetAxB";
                        case -337596917:
                            return "GetAxB";
                        case -365341286:
                            return "SetA";
                        case 1046971231:
                            return "SetB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1571726004 + ",methodId=" + @methodId);
                    }

                case -684597085:
                    switch (@methodId)
                    {
                        case -411561932:
                            return "GetA";
                        case 1039727631:
                            return "GetAxB";
                        case -337596917:
                            return "GetAxB";
                        case -365341286:
                            return "SetA";
                        case 1046971231:
                            return "SetB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -684597085 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<T> @GetA()
        {
            return base.@InvokeMethodAsync<T>(-411561932, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB()
        {
            return base.@InvokeMethodAsync<global::System.String>(1039727631, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB(T @a, U @b)
        {
            return base.@InvokeMethodAsync<global::System.String>(-337596917, new global::System.Object[]{@a, @b});
        }

        public global::System.Threading.Tasks.Task @SetA(T @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-365341286, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @SetB(U @b)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1046971231, new global::System.Object[]{@b});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T,U>", -684597085, typeof (global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSimpleGenericGrain2MethodInvoker<T, U> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1571726004:
                        switch (@methodId)
                        {
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@GetA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@GetAxB().@Box();
                            case -337596917:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@GetAxB((T)@arguments[0], (U)@arguments[1]).@Box();
                            case -365341286:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@SetA((T)@arguments[0]).@Box();
                            case 1046971231:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@SetB((U)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1571726004 + ",methodId=" + @methodId);
                        }

                    case -684597085:
                        switch (@methodId)
                        {
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@GetA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@GetAxB().@Box();
                            case -337596917:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@GetAxB((T)@arguments[0], (U)@arguments[1]).@Box();
                            case -365341286:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@SetA((T)@arguments[0]).@Box();
                            case 1046971231:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain2<T, U>)@grain).@SetB((U)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -684597085 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -684597085;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericGrainWithNoProperties<>))]
    internal class OrleansCodeGenGenericGrainWithNoPropertiesReference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericGrainWithNoProperties<T>
    {
        protected @OrleansCodeGenGenericGrainWithNoPropertiesReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericGrainWithNoPropertiesReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1337405522;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericGrainWithNoProperties<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1337405522 || @interfaceId == 1504548740;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1504548740:
                    switch (@methodId)
                    {
                        case 923307441:
                            return "GetAxB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1504548740 + ",methodId=" + @methodId);
                    }

                case 1337405522:
                    switch (@methodId)
                    {
                        case 923307441:
                            return "GetAxB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1337405522 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB(T @a, T @b)
        {
            return base.@InvokeMethodAsync<global::System.String>(923307441, new global::System.Object[]{@a, @b});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericGrainWithNoProperties<T>", 1337405522, typeof (global::UnitTests.GrainInterfaces.IGenericGrainWithNoProperties<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericGrainWithNoPropertiesMethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1504548740:
                        switch (@methodId)
                        {
                            case 923307441:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrainWithNoProperties<T>)@grain).@GetAxB((T)@arguments[0], (T)@arguments[1]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1504548740 + ",methodId=" + @methodId);
                        }

                    case 1337405522:
                        switch (@methodId)
                        {
                            case 923307441:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrainWithNoProperties<T>)@grain).@GetAxB((T)@arguments[0], (T)@arguments[1]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1337405522 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1337405522;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGrainWithNoProperties))]
    internal class OrleansCodeGenGrainWithNoPropertiesReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGrainWithNoProperties
    {
        protected @OrleansCodeGenGrainWithNoPropertiesReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGrainWithNoPropertiesReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1624077082;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGrainWithNoProperties";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1624077082;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1624077082:
                    switch (@methodId)
                    {
                        case 598136665:
                            return "GetAxB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1624077082 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB(global::System.Int32 @a, global::System.Int32 @b)
        {
            return base.@InvokeMethodAsync<global::System.String>(598136665, new global::System.Object[]{@a, @b});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGrainWithNoProperties", -1624077082, typeof (global::UnitTests.GrainInterfaces.IGrainWithNoProperties)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGrainWithNoPropertiesMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1624077082:
                        switch (@methodId)
                        {
                            case 598136665:
                                return ((global::UnitTests.GrainInterfaces.IGrainWithNoProperties)@grain).@GetAxB((global::System.Int32)@arguments[0], (global::System.Int32)@arguments[1]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1624077082 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1624077082;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGrainWithListFields))]
    internal class OrleansCodeGenGrainWithListFieldsReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGrainWithListFields
    {
        protected @OrleansCodeGenGrainWithListFieldsReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGrainWithListFieldsReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1210669940;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGrainWithListFields";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1210669940;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1210669940:
                    switch (@methodId)
                    {
                        case 331933939:
                            return "AddItem";
                        case 1745404428:
                            return "GetItems";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1210669940 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @AddItem(global::System.String @item)
        {
            return base.@InvokeMethodAsync<global::System.Object>(331933939, new global::System.Object[]{@item});
        }

        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::System.String>> @GetItems()
        {
            return base.@InvokeMethodAsync<global::System.Collections.Generic.IList<global::System.String>>(1745404428, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGrainWithListFields", -1210669940, typeof (global::UnitTests.GrainInterfaces.IGrainWithListFields)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGrainWithListFieldsMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1210669940:
                        switch (@methodId)
                        {
                            case 331933939:
                                return ((global::UnitTests.GrainInterfaces.IGrainWithListFields)@grain).@AddItem((global::System.String)@arguments[0]).@Box();
                            case 1745404428:
                                return ((global::UnitTests.GrainInterfaces.IGrainWithListFields)@grain).@GetItems().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1210669940 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1210669940;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericGrainWithListFields<>))]
    internal class OrleansCodeGenGenericGrainWithListFieldsReference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericGrainWithListFields<T>
    {
        protected @OrleansCodeGenGenericGrainWithListFieldsReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericGrainWithListFieldsReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -316450890;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericGrainWithListFields<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -316450890 || @interfaceId == -1699945270;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1699945270:
                    switch (@methodId)
                    {
                        case 841516755:
                            return "AddItem";
                        case 1745404428:
                            return "GetItems";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1699945270 + ",methodId=" + @methodId);
                    }

                case -316450890:
                    switch (@methodId)
                    {
                        case 841516755:
                            return "AddItem";
                        case 1745404428:
                            return "GetItems";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -316450890 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @AddItem(T @item)
        {
            return base.@InvokeMethodAsync<global::System.Object>(841516755, new global::System.Object[]{@item});
        }

        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<T>> @GetItems()
        {
            return base.@InvokeMethodAsync<global::System.Collections.Generic.IList<T>>(1745404428, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericGrainWithListFields<T>", -316450890, typeof (global::UnitTests.GrainInterfaces.IGenericGrainWithListFields<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericGrainWithListFieldsMethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1699945270:
                        switch (@methodId)
                        {
                            case 841516755:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrainWithListFields<T>)@grain).@AddItem((T)@arguments[0]).@Box();
                            case 1745404428:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrainWithListFields<T>)@grain).@GetItems().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1699945270 + ",methodId=" + @methodId);
                        }

                    case -316450890:
                        switch (@methodId)
                        {
                            case 841516755:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrainWithListFields<T>)@grain).@AddItem((T)@arguments[0]).@Box();
                            case 1745404428:
                                return ((global::UnitTests.GrainInterfaces.IGenericGrainWithListFields<T>)@grain).@GetItems().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -316450890 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -316450890;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericReader1<>))]
    internal class OrleansCodeGenGenericReader1Reference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericReader1<T>
    {
        protected @OrleansCodeGenGenericReader1Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericReader1Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 767537344;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericReader1<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 767537344 || @interfaceId == 1863418523;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1863418523:
                    switch (@methodId)
                    {
                        case 637921746:
                            return "GetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1863418523 + ",methodId=" + @methodId);
                    }

                case 767537344:
                    switch (@methodId)
                    {
                        case 637921746:
                            return "GetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 767537344 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<T> @GetValue()
        {
            return base.@InvokeMethodAsync<T>(637921746, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericReader1<T>", 767537344, typeof (global::UnitTests.GrainInterfaces.IGenericReader1<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericReader1MethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1863418523:
                        switch (@methodId)
                        {
                            case 637921746:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader1<T>)@grain).@GetValue().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1863418523 + ",methodId=" + @methodId);
                        }

                    case 767537344:
                        switch (@methodId)
                        {
                            case 637921746:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader1<T>)@grain).@GetValue().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 767537344 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 767537344;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericWriter1<>))]
    internal class OrleansCodeGenGenericWriter1Reference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericWriter1<T>
    {
        protected @OrleansCodeGenGenericWriter1Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericWriter1Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -134445539;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericWriter1<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -134445539 || @interfaceId == 1121461439;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1121461439:
                    switch (@methodId)
                    {
                        case -1058376616:
                            return "SetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1121461439 + ",methodId=" + @methodId);
                    }

                case -134445539:
                    switch (@methodId)
                    {
                        case -1058376616:
                            return "SetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -134445539 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetValue(T @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1058376616, new global::System.Object[]{@value});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericWriter1<T>", -134445539, typeof (global::UnitTests.GrainInterfaces.IGenericWriter1<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericWriter1MethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1121461439:
                        switch (@methodId)
                        {
                            case -1058376616:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter1<T>)@grain).@SetValue((T)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1121461439 + ",methodId=" + @methodId);
                        }

                    case -134445539:
                        switch (@methodId)
                        {
                            case -1058376616:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter1<T>)@grain).@SetValue((T)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -134445539 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -134445539;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<>))]
    internal class OrleansCodeGenGenericReaderWriterGrain1Reference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<T>
    {
        protected @OrleansCodeGenGenericReaderWriterGrain1Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericReaderWriterGrain1Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1909583157;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1909583157 || @interfaceId == 1290826449 || @interfaceId == 1121461439 || @interfaceId == 1863418523;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1290826449:
                    switch (@methodId)
                    {
                        case -1058376616:
                            return "SetValue";
                        case 637921746:
                            return "GetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1290826449 + ",methodId=" + @methodId);
                    }

                case 1121461439:
                    switch (@methodId)
                    {
                        case -1058376616:
                            return "SetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1121461439 + ",methodId=" + @methodId);
                    }

                case 1863418523:
                    switch (@methodId)
                    {
                        case 637921746:
                            return "GetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1863418523 + ",methodId=" + @methodId);
                    }

                case 1909583157:
                    switch (@methodId)
                    {
                        case -1058376616:
                            return "SetValue";
                        case 637921746:
                            return "GetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1909583157 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetValue(T @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1058376616, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task<T> @GetValue()
        {
            return base.@InvokeMethodAsync<T>(637921746, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<T>", 1909583157, typeof (global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericReaderWriterGrain1MethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1290826449:
                        switch (@methodId)
                        {
                            case -1058376616:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<T>)@grain).@SetValue((T)@arguments[0]).@Box();
                            case 637921746:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<T>)@grain).@GetValue().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1290826449 + ",methodId=" + @methodId);
                        }

                    case 1121461439:
                        switch (@methodId)
                        {
                            case -1058376616:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<T>)@grain).@SetValue((T)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1121461439 + ",methodId=" + @methodId);
                        }

                    case 1863418523:
                        switch (@methodId)
                        {
                            case 637921746:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<T>)@grain).@GetValue().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1863418523 + ",methodId=" + @methodId);
                        }

                    case 1909583157:
                        switch (@methodId)
                        {
                            case -1058376616:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<T>)@grain).@SetValue((T)@arguments[0]).@Box();
                            case 637921746:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain1<T>)@grain).@GetValue().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1909583157 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1909583157;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericReader2<, >))]
    internal class OrleansCodeGenGenericReader2Reference<TOne, TTwo> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericReader2<TOne, TTwo>
    {
        protected @OrleansCodeGenGenericReader2Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericReader2Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1685381360;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericReader2<TOne,TTwo>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1685381360 || @interfaceId == -1983889502;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1983889502:
                    switch (@methodId)
                    {
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1983889502 + ",methodId=" + @methodId);
                    }

                case 1685381360:
                    switch (@methodId)
                    {
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1685381360 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<TOne> @GetValue1()
        {
            return base.@InvokeMethodAsync<TOne>(-557414356, null);
        }

        public global::System.Threading.Tasks.Task<TTwo> @GetValue2()
        {
            return base.@InvokeMethodAsync<TTwo>(1855368955, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericReader2<TOne,TTwo>", 1685381360, typeof (global::UnitTests.GrainInterfaces.IGenericReader2<, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericReader2MethodInvoker<TOne, TTwo> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1983889502:
                        switch (@methodId)
                        {
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader2<TOne, TTwo>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader2<TOne, TTwo>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1983889502 + ",methodId=" + @methodId);
                        }

                    case 1685381360:
                        switch (@methodId)
                        {
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader2<TOne, TTwo>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader2<TOne, TTwo>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1685381360 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1685381360;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericWriter2<, >))]
    internal class OrleansCodeGenGenericWriter2Reference<TOne, TTwo> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericWriter2<TOne, TTwo>
    {
        protected @OrleansCodeGenGenericWriter2Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericWriter2Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -405042587;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericWriter2<TOne,TTwo>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -405042587 || @interfaceId == 131249727;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 131249727:
                    switch (@methodId)
                    {
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 131249727 + ",methodId=" + @methodId);
                    }

                case -405042587:
                    switch (@methodId)
                    {
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -405042587 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetValue1(TOne @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1200762245, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task @SetValue2(TTwo @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-115270010, new global::System.Object[]{@value});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericWriter2<TOne,TTwo>", -405042587, typeof (global::UnitTests.GrainInterfaces.IGenericWriter2<, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericWriter2MethodInvoker<TOne, TTwo> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 131249727:
                        switch (@methodId)
                        {
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter2<TOne, TTwo>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter2<TOne, TTwo>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 131249727 + ",methodId=" + @methodId);
                        }

                    case -405042587:
                        switch (@methodId)
                        {
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter2<TOne, TTwo>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter2<TOne, TTwo>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -405042587 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -405042587;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<, >))]
    internal class OrleansCodeGenGenericReaderWriterGrain2Reference<TOne, TTwo> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>
    {
        protected @OrleansCodeGenGenericReaderWriterGrain2Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericReaderWriterGrain2Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1911654391;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne,TTwo>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1911654391 || @interfaceId == 1957933507 || @interfaceId == 131249727 || @interfaceId == -1983889502;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1957933507:
                    switch (@methodId)
                    {
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1957933507 + ",methodId=" + @methodId);
                    }

                case 131249727:
                    switch (@methodId)
                    {
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 131249727 + ",methodId=" + @methodId);
                    }

                case -1983889502:
                    switch (@methodId)
                    {
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1983889502 + ",methodId=" + @methodId);
                    }

                case 1911654391:
                    switch (@methodId)
                    {
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1911654391 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetValue1(TOne @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1200762245, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task @SetValue2(TTwo @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-115270010, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task<TOne> @GetValue1()
        {
            return base.@InvokeMethodAsync<TOne>(-557414356, null);
        }

        public global::System.Threading.Tasks.Task<TTwo> @GetValue2()
        {
            return base.@InvokeMethodAsync<TTwo>(1855368955, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne,TTwo>", 1911654391, typeof (global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericReaderWriterGrain2MethodInvoker<TOne, TTwo> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1957933507:
                        switch (@methodId)
                        {
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1957933507 + ",methodId=" + @methodId);
                        }

                    case 131249727:
                        switch (@methodId)
                        {
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 131249727 + ",methodId=" + @methodId);
                        }

                    case -1983889502:
                        switch (@methodId)
                        {
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1983889502 + ",methodId=" + @methodId);
                        }

                    case 1911654391:
                        switch (@methodId)
                        {
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain2<TOne, TTwo>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1911654391 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1911654391;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericReader3<,, >))]
    internal class OrleansCodeGenGenericReader3Reference<TOne, TTwo, TThree> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericReader3<TOne, TTwo, TThree>
    {
        protected @OrleansCodeGenGenericReader3Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericReader3Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1357988267;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericReader3<TOne,TTwo,TThree>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1357988267 || @interfaceId == 1353040013 || @interfaceId == -1983889502;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1353040013:
                    switch (@methodId)
                    {
                        case 2034009230:
                            return "GetValue3";
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1353040013 + ",methodId=" + @methodId);
                    }

                case -1983889502:
                    switch (@methodId)
                    {
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1983889502 + ",methodId=" + @methodId);
                    }

                case -1357988267:
                    switch (@methodId)
                    {
                        case 2034009230:
                            return "GetValue3";
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1357988267 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<TThree> @GetValue3()
        {
            return base.@InvokeMethodAsync<TThree>(2034009230, null);
        }

        public global::System.Threading.Tasks.Task<TOne> @GetValue1()
        {
            return base.@InvokeMethodAsync<TOne>(-557414356, null);
        }

        public global::System.Threading.Tasks.Task<TTwo> @GetValue2()
        {
            return base.@InvokeMethodAsync<TTwo>(1855368955, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericReader3<TOne,TTwo,TThree>", -1357988267, typeof (global::UnitTests.GrainInterfaces.IGenericReader3<,, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericReader3MethodInvoker<TOne, TTwo, TThree> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1353040013:
                        switch (@methodId)
                        {
                            case 2034009230:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader3<TOne, TTwo, TThree>)@grain).@GetValue3().@Box();
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader3<TOne, TTwo, TThree>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader3<TOne, TTwo, TThree>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1353040013 + ",methodId=" + @methodId);
                        }

                    case -1983889502:
                        switch (@methodId)
                        {
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader3<TOne, TTwo, TThree>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader3<TOne, TTwo, TThree>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1983889502 + ",methodId=" + @methodId);
                        }

                    case -1357988267:
                        switch (@methodId)
                        {
                            case 2034009230:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader3<TOne, TTwo, TThree>)@grain).@GetValue3().@Box();
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader3<TOne, TTwo, TThree>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReader3<TOne, TTwo, TThree>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1357988267 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1357988267;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericWriter3<,, >))]
    internal class OrleansCodeGenGenericWriter3Reference<TOne, TTwo, TThree> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericWriter3<TOne, TTwo, TThree>
    {
        protected @OrleansCodeGenGenericWriter3Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericWriter3Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1320810526;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericWriter3<TOne,TTwo,TThree>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1320810526 || @interfaceId == 518608517 || @interfaceId == 131249727;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 518608517:
                    switch (@methodId)
                    {
                        case -1203968909:
                            return "SetValue3";
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 518608517 + ",methodId=" + @methodId);
                    }

                case 131249727:
                    switch (@methodId)
                    {
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 131249727 + ",methodId=" + @methodId);
                    }

                case 1320810526:
                    switch (@methodId)
                    {
                        case -1203968909:
                            return "SetValue3";
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1320810526 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetValue3(TThree @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1203968909, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task @SetValue1(TOne @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1200762245, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task @SetValue2(TTwo @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-115270010, new global::System.Object[]{@value});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericWriter3<TOne,TTwo,TThree>", 1320810526, typeof (global::UnitTests.GrainInterfaces.IGenericWriter3<,, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericWriter3MethodInvoker<TOne, TTwo, TThree> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 518608517:
                        switch (@methodId)
                        {
                            case -1203968909:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter3<TOne, TTwo, TThree>)@grain).@SetValue3((TThree)@arguments[0]).@Box();
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter3<TOne, TTwo, TThree>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter3<TOne, TTwo, TThree>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 518608517 + ",methodId=" + @methodId);
                        }

                    case 131249727:
                        switch (@methodId)
                        {
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter3<TOne, TTwo, TThree>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter3<TOne, TTwo, TThree>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 131249727 + ",methodId=" + @methodId);
                        }

                    case 1320810526:
                        switch (@methodId)
                        {
                            case -1203968909:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter3<TOne, TTwo, TThree>)@grain).@SetValue3((TThree)@arguments[0]).@Box();
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter3<TOne, TTwo, TThree>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericWriter3<TOne, TTwo, TThree>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1320810526 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1320810526;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<,, >))]
    internal class OrleansCodeGenGenericReaderWriterGrain3Reference<TOne, TTwo, TThree> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>
    {
        protected @OrleansCodeGenGenericReaderWriterGrain3Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericReaderWriterGrain3Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -689214647;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne,TTwo,TThree>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -689214647 || @interfaceId == 646122141 || @interfaceId == 518608517 || @interfaceId == 131249727 || @interfaceId == 1353040013 || @interfaceId == -1983889502;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 646122141:
                    switch (@methodId)
                    {
                        case -1203968909:
                            return "SetValue3";
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        case 2034009230:
                            return "GetValue3";
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 646122141 + ",methodId=" + @methodId);
                    }

                case 518608517:
                    switch (@methodId)
                    {
                        case -1203968909:
                            return "SetValue3";
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 518608517 + ",methodId=" + @methodId);
                    }

                case 131249727:
                    switch (@methodId)
                    {
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 131249727 + ",methodId=" + @methodId);
                    }

                case 1353040013:
                    switch (@methodId)
                    {
                        case 2034009230:
                            return "GetValue3";
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1353040013 + ",methodId=" + @methodId);
                    }

                case -1983889502:
                    switch (@methodId)
                    {
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1983889502 + ",methodId=" + @methodId);
                    }

                case -689214647:
                    switch (@methodId)
                    {
                        case -1203968909:
                            return "SetValue3";
                        case 1200762245:
                            return "SetValue1";
                        case -115270010:
                            return "SetValue2";
                        case 2034009230:
                            return "GetValue3";
                        case -557414356:
                            return "GetValue1";
                        case 1855368955:
                            return "GetValue2";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -689214647 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetValue3(TThree @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1203968909, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task @SetValue1(TOne @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1200762245, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task @SetValue2(TTwo @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-115270010, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task<TThree> @GetValue3()
        {
            return base.@InvokeMethodAsync<TThree>(2034009230, null);
        }

        public global::System.Threading.Tasks.Task<TOne> @GetValue1()
        {
            return base.@InvokeMethodAsync<TOne>(-557414356, null);
        }

        public global::System.Threading.Tasks.Task<TTwo> @GetValue2()
        {
            return base.@InvokeMethodAsync<TTwo>(1855368955, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne,TTwo,TThree>", -689214647, typeof (global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<,, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericReaderWriterGrain3MethodInvoker<TOne, TTwo, TThree> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 646122141:
                        switch (@methodId)
                        {
                            case -1203968909:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue3((TThree)@arguments[0]).@Box();
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            case 2034009230:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue3().@Box();
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 646122141 + ",methodId=" + @methodId);
                        }

                    case 518608517:
                        switch (@methodId)
                        {
                            case -1203968909:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue3((TThree)@arguments[0]).@Box();
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 518608517 + ",methodId=" + @methodId);
                        }

                    case 131249727:
                        switch (@methodId)
                        {
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 131249727 + ",methodId=" + @methodId);
                        }

                    case 1353040013:
                        switch (@methodId)
                        {
                            case 2034009230:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue3().@Box();
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1353040013 + ",methodId=" + @methodId);
                        }

                    case -1983889502:
                        switch (@methodId)
                        {
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1983889502 + ",methodId=" + @methodId);
                        }

                    case -689214647:
                        switch (@methodId)
                        {
                            case -1203968909:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue3((TThree)@arguments[0]).@Box();
                            case 1200762245:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue1((TOne)@arguments[0]).@Box();
                            case -115270010:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@SetValue2((TTwo)@arguments[0]).@Box();
                            case 2034009230:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue3().@Box();
                            case -557414356:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue1().@Box();
                            case 1855368955:
                                return ((global::UnitTests.GrainInterfaces.IGenericReaderWriterGrain3<TOne, TTwo, TThree>)@grain).@GetValue2().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -689214647 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -689214647;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<, >))]
    internal class OrleansCodeGenGenericSelfManagedGrainReference<T, U> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>
    {
        protected @OrleansCodeGenGenericSelfManagedGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericSelfManagedGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1720327813;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T,U>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1720327813 || @interfaceId == 1828585624;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1828585624:
                    switch (@methodId)
                    {
                        case -411561932:
                            return "GetA";
                        case 1039727631:
                            return "GetAxB";
                        case -337596917:
                            return "GetAxB";
                        case -365341286:
                            return "SetA";
                        case 1046971231:
                            return "SetB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1828585624 + ",methodId=" + @methodId);
                    }

                case 1720327813:
                    switch (@methodId)
                    {
                        case -411561932:
                            return "GetA";
                        case 1039727631:
                            return "GetAxB";
                        case -337596917:
                            return "GetAxB";
                        case -365341286:
                            return "SetA";
                        case 1046971231:
                            return "SetB";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1720327813 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<T> @GetA()
        {
            return base.@InvokeMethodAsync<T>(-411561932, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB()
        {
            return base.@InvokeMethodAsync<global::System.String>(1039727631, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetAxB(T @a, U @b)
        {
            return base.@InvokeMethodAsync<global::System.String>(-337596917, new global::System.Object[]{@a, @b});
        }

        public global::System.Threading.Tasks.Task @SetA(T @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-365341286, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @SetB(U @b)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1046971231, new global::System.Object[]{@b});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T,U>", 1720327813, typeof (global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericSelfManagedGrainMethodInvoker<T, U> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1828585624:
                        switch (@methodId)
                        {
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@GetA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@GetAxB().@Box();
                            case -337596917:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@GetAxB((T)@arguments[0], (U)@arguments[1]).@Box();
                            case -365341286:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@SetA((T)@arguments[0]).@Box();
                            case 1046971231:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@SetB((U)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1828585624 + ",methodId=" + @methodId);
                        }

                    case 1720327813:
                        switch (@methodId)
                        {
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@GetA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@GetAxB().@Box();
                            case -337596917:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@GetAxB((T)@arguments[0], (U)@arguments[1]).@Box();
                            case -365341286:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@SetA((T)@arguments[0]).@Box();
                            case 1046971231:
                                return ((global::UnitTests.GrainInterfaces.IGenericSelfManagedGrain<T, U>)@grain).@SetB((U)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1720327813 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1720327813;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IHubGrain<,, >))]
    internal class OrleansCodeGenHubGrainReference<TKey, T1, T2> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IHubGrain<TKey, T1, T2>
    {
        protected @OrleansCodeGenHubGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenHubGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 300696295;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IHubGrain<TKey,T1,T2>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 300696295 || @interfaceId == -1181493523;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1181493523:
                    switch (@methodId)
                    {
                        case -1974697792:
                            return "Bar";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1181493523 + ",methodId=" + @methodId);
                    }

                case 300696295:
                    switch (@methodId)
                    {
                        case -1974697792:
                            return "Bar";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 300696295 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @Bar(TKey @key, T1 @message1, T2 @message2)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1974697792, new global::System.Object[]{@key, @message1, @message2});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IHubGrain<TKey,T1,T2>", 300696295, typeof (global::UnitTests.GrainInterfaces.IHubGrain<,, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenHubGrainMethodInvoker<TKey, T1, T2> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1181493523:
                        switch (@methodId)
                        {
                            case -1974697792:
                                return ((global::UnitTests.GrainInterfaces.IHubGrain<TKey, T1, T2>)@grain).@Bar((TKey)@arguments[0], (T1)@arguments[1], (T2)@arguments[2]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1181493523 + ",methodId=" + @methodId);
                        }

                    case 300696295:
                        switch (@methodId)
                        {
                            case -1974697792:
                                return ((global::UnitTests.GrainInterfaces.IHubGrain<TKey, T1, T2>)@grain).@Bar((TKey)@arguments[0], (T1)@arguments[1], (T2)@arguments[2]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 300696295 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 300696295;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IEchoHubGrain<, >))]
    internal class OrleansCodeGenEchoHubGrainReference<TKey, TMessage> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey, TMessage>
    {
        protected @OrleansCodeGenEchoHubGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenEchoHubGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1731827949;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey,TMessage>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1731827949 || @interfaceId == 1571654530 || @interfaceId == -1181493523;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1571654530:
                    switch (@methodId)
                    {
                        case -119836414:
                            return "Foo";
                        case 1208721954:
                            return "GetX";
                        case 147404757:
                            return "Bar";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1571654530 + ",methodId=" + @methodId);
                    }

                case -1181493523:
                    switch (@methodId)
                    {
                        case 147404757:
                            return "Bar";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1181493523 + ",methodId=" + @methodId);
                    }

                case -1731827949:
                    switch (@methodId)
                    {
                        case -119836414:
                            return "Foo";
                        case 1208721954:
                            return "GetX";
                        case 147404757:
                            return "Bar";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1731827949 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @Foo(TKey @key, TMessage @message, global::System.Int32 @x)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-119836414, new global::System.Object[]{@key, @message, @x});
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetX()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(1208721954, null);
        }

        public global::System.Threading.Tasks.Task @Bar(TKey @key, TMessage @message1, TMessage @message2)
        {
            return base.@InvokeMethodAsync<global::System.Object>(147404757, new global::System.Object[]{@key, @message1, @message2});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey,TMessage>", -1731827949, typeof (global::UnitTests.GrainInterfaces.IEchoHubGrain<, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenEchoHubGrainMethodInvoker<TKey, TMessage> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1571654530:
                        switch (@methodId)
                        {
                            case -119836414:
                                return ((global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey, TMessage>)@grain).@Foo((TKey)@arguments[0], (TMessage)@arguments[1], (global::System.Int32)@arguments[2]).@Box();
                            case 1208721954:
                                return ((global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey, TMessage>)@grain).@GetX().@Box();
                            case 147404757:
                                return ((global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey, TMessage>)@grain).@Bar((TKey)@arguments[0], (TMessage)@arguments[1], (TMessage)@arguments[2]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1571654530 + ",methodId=" + @methodId);
                        }

                    case -1181493523:
                        switch (@methodId)
                        {
                            case 147404757:
                                return ((global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey, TMessage>)@grain).@Bar((TKey)@arguments[0], (TMessage)@arguments[1], (TMessage)@arguments[2]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1181493523 + ",methodId=" + @methodId);
                        }

                    case -1731827949:
                        switch (@methodId)
                        {
                            case -119836414:
                                return ((global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey, TMessage>)@grain).@Foo((TKey)@arguments[0], (TMessage)@arguments[1], (global::System.Int32)@arguments[2]).@Box();
                            case 1208721954:
                                return ((global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey, TMessage>)@grain).@GetX().@Box();
                            case 147404757:
                                return ((global::UnitTests.GrainInterfaces.IEchoHubGrain<TKey, TMessage>)@grain).@Bar((TKey)@arguments[0], (TMessage)@arguments[1], (TMessage)@arguments[2]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1731827949 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1731827949;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<>))]
    internal class OrleansCodeGenEchoGenericChainGrainReference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>
    {
        protected @OrleansCodeGenEchoGenericChainGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenEchoGenericChainGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 345675939;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 345675939 || @interfaceId == 2054284115;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 2054284115:
                    switch (@methodId)
                    {
                        case 1341228101:
                            return "Echo";
                        case -7571017:
                            return "Echo2";
                        case 48568241:
                            return "Echo3";
                        case 2091432811:
                            return "Echo4";
                        case -321766269:
                            return "Echo5";
                        case -850106523:
                            return "Echo6";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 2054284115 + ",methodId=" + @methodId);
                    }

                case 345675939:
                    switch (@methodId)
                    {
                        case 1341228101:
                            return "Echo";
                        case -7571017:
                            return "Echo2";
                        case 48568241:
                            return "Echo3";
                        case 2091432811:
                            return "Echo4";
                        case -321766269:
                            return "Echo5";
                        case -850106523:
                            return "Echo6";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 345675939 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<T> @Echo(T @item)
        {
            return base.@InvokeMethodAsync<T>(1341228101, new global::System.Object[]{@item});
        }

        public global::System.Threading.Tasks.Task<T> @Echo2(T @item)
        {
            return base.@InvokeMethodAsync<T>(-7571017, new global::System.Object[]{@item});
        }

        public global::System.Threading.Tasks.Task<T> @Echo3(T @item)
        {
            return base.@InvokeMethodAsync<T>(48568241, new global::System.Object[]{@item});
        }

        public global::System.Threading.Tasks.Task<T> @Echo4(T @item)
        {
            return base.@InvokeMethodAsync<T>(2091432811, new global::System.Object[]{@item});
        }

        public global::System.Threading.Tasks.Task<T> @Echo5(T @item)
        {
            return base.@InvokeMethodAsync<T>(-321766269, new global::System.Object[]{@item});
        }

        public global::System.Threading.Tasks.Task<T> @Echo6(T @item)
        {
            return base.@InvokeMethodAsync<T>(-850106523, new global::System.Object[]{@item});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>", 345675939, typeof (global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenEchoGenericChainGrainMethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 2054284115:
                        switch (@methodId)
                        {
                            case 1341228101:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo((T)@arguments[0]).@Box();
                            case -7571017:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo2((T)@arguments[0]).@Box();
                            case 48568241:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo3((T)@arguments[0]).@Box();
                            case 2091432811:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo4((T)@arguments[0]).@Box();
                            case -321766269:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo5((T)@arguments[0]).@Box();
                            case -850106523:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo6((T)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 2054284115 + ",methodId=" + @methodId);
                        }

                    case 345675939:
                        switch (@methodId)
                        {
                            case 1341228101:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo((T)@arguments[0]).@Box();
                            case -7571017:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo2((T)@arguments[0]).@Box();
                            case 48568241:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo3((T)@arguments[0]).@Box();
                            case 2091432811:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo4((T)@arguments[0]).@Box();
                            case -321766269:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo5((T)@arguments[0]).@Box();
                            case -850106523:
                                return ((global::UnitTests.GrainInterfaces.IEchoGenericChainGrain<T>)@grain).@Echo6((T)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 345675939 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 345675939;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.INonGenericBase))]
    internal class OrleansCodeGenNonGenericBaseReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.INonGenericBase
    {
        protected @OrleansCodeGenNonGenericBaseReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenNonGenericBaseReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 2104871304;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.INonGenericBase";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 2104871304;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 2104871304:
                    switch (@methodId)
                    {
                        case -640371947:
                            return "Ping";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 2104871304 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @Ping()
        {
            return base.@InvokeMethodAsync<global::System.Object>(-640371947, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.INonGenericBase", 2104871304, typeof (global::UnitTests.GrainInterfaces.INonGenericBase)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenNonGenericBaseMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 2104871304:
                        switch (@methodId)
                        {
                            case -640371947:
                                return ((global::UnitTests.GrainInterfaces.INonGenericBase)@grain).@Ping().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 2104871304 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 2104871304;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGeneric1Argument<>))]
    internal class OrleansCodeGenGeneric1ArgumentReference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGeneric1Argument<T>
    {
        protected @OrleansCodeGenGeneric1ArgumentReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGeneric1ArgumentReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -340585198;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGeneric1Argument<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -340585198 || @interfaceId == 29270695;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 29270695:
                    switch (@methodId)
                    {
                        case 596862988:
                            return "Ping";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 29270695 + ",methodId=" + @methodId);
                    }

                case -340585198:
                    switch (@methodId)
                    {
                        case 596862988:
                            return "Ping";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -340585198 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<T> @Ping(T @t)
        {
            return base.@InvokeMethodAsync<T>(596862988, new global::System.Object[]{@t});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGeneric1Argument<T>", -340585198, typeof (global::UnitTests.GrainInterfaces.IGeneric1Argument<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGeneric1ArgumentMethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 29270695:
                        switch (@methodId)
                        {
                            case 596862988:
                                return ((global::UnitTests.GrainInterfaces.IGeneric1Argument<T>)@grain).@Ping((T)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 29270695 + ",methodId=" + @methodId);
                        }

                    case -340585198:
                        switch (@methodId)
                        {
                            case 596862988:
                                return ((global::UnitTests.GrainInterfaces.IGeneric1Argument<T>)@grain).@Ping((T)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -340585198 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -340585198;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGeneric2Arguments<, >))]
    internal class OrleansCodeGenGeneric2ArgumentsReference<T, U> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGeneric2Arguments<T, U>
    {
        protected @OrleansCodeGenGeneric2ArgumentsReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGeneric2ArgumentsReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1208173962;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGeneric2Arguments<T,U>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1208173962 || @interfaceId == -2096972775;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -2096972775:
                    switch (@methodId)
                    {
                        case -417857976:
                            return "Ping";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -2096972775 + ",methodId=" + @methodId);
                    }

                case 1208173962:
                    switch (@methodId)
                    {
                        case -417857976:
                            return "Ping";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1208173962 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::System.Tuple<T, U>> @Ping(T @t, U @u)
        {
            return base.@InvokeMethodAsync<global::System.Tuple<T, U>>(-417857976, new global::System.Object[]{@t, @u});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGeneric2Arguments<T,U>", 1208173962, typeof (global::UnitTests.GrainInterfaces.IGeneric2Arguments<, >)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGeneric2ArgumentsMethodInvoker<T, U> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -2096972775:
                        switch (@methodId)
                        {
                            case -417857976:
                                return ((global::UnitTests.GrainInterfaces.IGeneric2Arguments<T, U>)@grain).@Ping((T)@arguments[0], (U)@arguments[1]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -2096972775 + ",methodId=" + @methodId);
                        }

                    case 1208173962:
                        switch (@methodId)
                        {
                            case -417857976:
                                return ((global::UnitTests.GrainInterfaces.IGeneric2Arguments<T, U>)@grain).@Ping((T)@arguments[0], (U)@arguments[1]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1208173962 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1208173962;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IDbGrain<>))]
    internal class OrleansCodeGenDbGrainReference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IDbGrain<T>
    {
        protected @OrleansCodeGenDbGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenDbGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 808581941;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IDbGrain<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 808581941 || @interfaceId == -2076394039;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -2076394039:
                    switch (@methodId)
                    {
                        case -1058376616:
                            return "SetValue";
                        case 637921746:
                            return "GetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -2076394039 + ",methodId=" + @methodId);
                    }

                case 808581941:
                    switch (@methodId)
                    {
                        case -1058376616:
                            return "SetValue";
                        case 637921746:
                            return "GetValue";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 808581941 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetValue(T @value)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1058376616, new global::System.Object[]{@value});
        }

        public global::System.Threading.Tasks.Task<T> @GetValue()
        {
            return base.@InvokeMethodAsync<T>(637921746, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IDbGrain<T>", 808581941, typeof (global::UnitTests.GrainInterfaces.IDbGrain<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenDbGrainMethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -2076394039:
                        switch (@methodId)
                        {
                            case -1058376616:
                                return ((global::UnitTests.GrainInterfaces.IDbGrain<T>)@grain).@SetValue((T)@arguments[0]).@Box();
                            case 637921746:
                                return ((global::UnitTests.GrainInterfaces.IDbGrain<T>)@grain).@GetValue().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -2076394039 + ",methodId=" + @methodId);
                        }

                    case 808581941:
                        switch (@methodId)
                        {
                            case -1058376616:
                                return ((global::UnitTests.GrainInterfaces.IDbGrain<T>)@grain).@SetValue((T)@arguments[0]).@Box();
                            case 637921746:
                                return ((global::UnitTests.GrainInterfaces.IDbGrain<T>)@grain).@GetValue().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 808581941 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 808581941;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IGenericPingSelf<>))]
    internal class OrleansCodeGenGenericPingSelfReference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IGenericPingSelf<T>
    {
        protected @OrleansCodeGenGenericPingSelfReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGenericPingSelfReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -654198331;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IGenericPingSelf<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -654198331 || @interfaceId == 1126788905;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1126788905:
                    switch (@methodId)
                    {
                        case 596862988:
                            return "Ping";
                        case 2017856506:
                            return "PingSelf";
                        case -1156227886:
                            return "PingOther";
                        case -973517959:
                            return "PingSelfThroughOther";
                        case 1940689642:
                            return "GetLastValue";
                        case 1707394585:
                            return "ScheduleDelayedPing";
                        case 1503453749:
                            return "ScheduleDelayedPingToSelfAndDeactivate";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1126788905 + ",methodId=" + @methodId);
                    }

                case -654198331:
                    switch (@methodId)
                    {
                        case 596862988:
                            return "Ping";
                        case 2017856506:
                            return "PingSelf";
                        case -1156227886:
                            return "PingOther";
                        case -973517959:
                            return "PingSelfThroughOther";
                        case 1940689642:
                            return "GetLastValue";
                        case 1707394585:
                            return "ScheduleDelayedPing";
                        case 1503453749:
                            return "ScheduleDelayedPingToSelfAndDeactivate";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -654198331 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<T> @Ping(T @t)
        {
            return base.@InvokeMethodAsync<T>(596862988, new global::System.Object[]{@t});
        }

        public global::System.Threading.Tasks.Task<T> @PingSelf(T @t)
        {
            return base.@InvokeMethodAsync<T>(2017856506, new global::System.Object[]{@t});
        }

        public global::System.Threading.Tasks.Task<T> @PingOther(global::UnitTests.GrainInterfaces.IGenericPingSelf<T> @target, T @t)
        {
            return base.@InvokeMethodAsync<T>(-1156227886, new global::System.Object[]{@target is global::Orleans.Grain ? @target.@AsReference<global::UnitTests.GrainInterfaces.IGenericPingSelf<T>>() : @target, @t});
        }

        public global::System.Threading.Tasks.Task<T> @PingSelfThroughOther(global::UnitTests.GrainInterfaces.IGenericPingSelf<T> @target, T @t)
        {
            return base.@InvokeMethodAsync<T>(-973517959, new global::System.Object[]{@target is global::Orleans.Grain ? @target.@AsReference<global::UnitTests.GrainInterfaces.IGenericPingSelf<T>>() : @target, @t});
        }

        public global::System.Threading.Tasks.Task<T> @GetLastValue()
        {
            return base.@InvokeMethodAsync<T>(1940689642, null);
        }

        public global::System.Threading.Tasks.Task @ScheduleDelayedPing(global::UnitTests.GrainInterfaces.IGenericPingSelf<T> @target, T @t, global::System.TimeSpan @delay)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1707394585, new global::System.Object[]{@target is global::Orleans.Grain ? @target.@AsReference<global::UnitTests.GrainInterfaces.IGenericPingSelf<T>>() : @target, @t, @delay});
        }

        public global::System.Threading.Tasks.Task @ScheduleDelayedPingToSelfAndDeactivate(global::UnitTests.GrainInterfaces.IGenericPingSelf<T> @target, T @t, global::System.TimeSpan @delay)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1503453749, new global::System.Object[]{@target is global::Orleans.Grain ? @target.@AsReference<global::UnitTests.GrainInterfaces.IGenericPingSelf<T>>() : @target, @t, @delay});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IGenericPingSelf<T>", -654198331, typeof (global::UnitTests.GrainInterfaces.IGenericPingSelf<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGenericPingSelfMethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1126788905:
                        switch (@methodId)
                        {
                            case 596862988:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@Ping((T)@arguments[0]).@Box();
                            case 2017856506:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@PingSelf((T)@arguments[0]).@Box();
                            case -1156227886:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@PingOther((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@arguments[0], (T)@arguments[1]).@Box();
                            case -973517959:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@PingSelfThroughOther((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@arguments[0], (T)@arguments[1]).@Box();
                            case 1940689642:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@GetLastValue().@Box();
                            case 1707394585:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@ScheduleDelayedPing((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@arguments[0], (T)@arguments[1], (global::System.TimeSpan)@arguments[2]).@Box();
                            case 1503453749:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@ScheduleDelayedPingToSelfAndDeactivate((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@arguments[0], (T)@arguments[1], (global::System.TimeSpan)@arguments[2]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1126788905 + ",methodId=" + @methodId);
                        }

                    case -654198331:
                        switch (@methodId)
                        {
                            case 596862988:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@Ping((T)@arguments[0]).@Box();
                            case 2017856506:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@PingSelf((T)@arguments[0]).@Box();
                            case -1156227886:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@PingOther((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@arguments[0], (T)@arguments[1]).@Box();
                            case -973517959:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@PingSelfThroughOther((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@arguments[0], (T)@arguments[1]).@Box();
                            case 1940689642:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@GetLastValue().@Box();
                            case 1707394585:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@ScheduleDelayedPing((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@arguments[0], (T)@arguments[1], (global::System.TimeSpan)@arguments[2]).@Box();
                            case 1503453749:
                                return ((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@grain).@ScheduleDelayedPingToSelfAndDeactivate((global::UnitTests.GrainInterfaces.IGenericPingSelf<T>)@arguments[0], (T)@arguments[1], (global::System.TimeSpan)@arguments[2]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -654198331 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -654198331;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain))]
    internal class OrleansCodeGenMultipleSubscriptionConsumerGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain
    {
        protected @OrleansCodeGenMultipleSubscriptionConsumerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenMultipleSubscriptionConsumerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1066298859;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1066298859;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1066298859:
                    switch (@methodId)
                    {
                        case 210218485:
                            return "BecomeConsumer";
                        case -966092664:
                            return "Resume";
                        case 469928104:
                            return "StopConsuming";
                        case -2026651720:
                            return "GetAllSubscriptions";
                        case 2130169286:
                            return "GetNumberConsumed";
                        case 1012128928:
                            return "ClearNumberConsumed";
                        case 1834577625:
                            return "Deactivate";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1066298859 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>> @BecomeConsumer(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>>(210218485, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>> @Resume(global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32> @handle)
        {
            return base.@InvokeMethodAsync<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>>(-966092664, new global::System.Object[]{@handle});
        }

        public global::System.Threading.Tasks.Task @StopConsuming(global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32> @handle)
        {
            return base.@InvokeMethodAsync<global::System.Object>(469928104, new global::System.Object[]{@handle});
        }

        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>>> @GetAllSubscriptions(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::System.Collections.Generic.IList<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>>>(-2026651720, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.Dictionary<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>, global::System.Int32>> @GetNumberConsumed()
        {
            return base.@InvokeMethodAsync<global::System.Collections.Generic.Dictionary<global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>, global::System.Int32>>(2130169286, null);
        }

        public global::System.Threading.Tasks.Task @ClearNumberConsumed()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1012128928, null);
        }

        public global::System.Threading.Tasks.Task @Deactivate()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1834577625, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain", -1066298859, typeof (global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenMultipleSubscriptionConsumerGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1066298859:
                        switch (@methodId)
                        {
                            case 210218485:
                                return ((global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain)@grain).@BecomeConsumer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case -966092664:
                                return ((global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain)@grain).@Resume((global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>)@arguments[0]).@Box();
                            case 469928104:
                                return ((global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain)@grain).@StopConsuming((global::Orleans.Streams.StreamSubscriptionHandle<global::System.Int32>)@arguments[0]).@Box();
                            case -2026651720:
                                return ((global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain)@grain).@GetAllSubscriptions((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 2130169286:
                                return ((global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain)@grain).@GetNumberConsumed().@Box();
                            case 1012128928:
                                return ((global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain)@grain).@ClearNumberConsumed().@Box();
                            case 1834577625:
                                return ((global::UnitTests.GrainInterfaces.IMultipleSubscriptionConsumerGrain)@grain).@Deactivate().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1066298859 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1066298859;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain))]
    internal class OrleansCodeGenSampleStreaming_ProducerGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain
    {
        protected @OrleansCodeGenSampleStreaming_ProducerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSampleStreaming_ProducerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1136982742;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1136982742;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1136982742:
                    switch (@methodId)
                    {
                        case 266083571:
                            return "BecomeProducer";
                        case -2001831838:
                            return "StartPeriodicProducing";
                        case 755705728:
                            return "StopPeriodicProducing";
                        case -970329735:
                            return "GetNumberProduced";
                        case 1732143298:
                            return "ClearNumberProduced";
                        case 1996957051:
                            return "Produce";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1136982742 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @BecomeProducer(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::System.Object>(266083571, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task @StartPeriodicProducing()
        {
            return base.@InvokeMethodAsync<global::System.Object>(-2001831838, null);
        }

        public global::System.Threading.Tasks.Task @StopPeriodicProducing()
        {
            return base.@InvokeMethodAsync<global::System.Object>(755705728, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberProduced()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-970329735, null);
        }

        public global::System.Threading.Tasks.Task @ClearNumberProduced()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1732143298, null);
        }

        public global::System.Threading.Tasks.Task @Produce()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1996957051, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain", 1136982742, typeof (global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSampleStreaming_ProducerGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1136982742:
                        switch (@methodId)
                        {
                            case 266083571:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain)@grain).@BecomeProducer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case -2001831838:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain)@grain).@StartPeriodicProducing().@Box();
                            case 755705728:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain)@grain).@StopPeriodicProducing().@Box();
                            case -970329735:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain)@grain).@GetNumberProduced().@Box();
                            case 1732143298:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain)@grain).@ClearNumberProduced().@Box();
                            case 1996957051:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_ProducerGrain)@grain).@Produce().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1136982742 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1136982742;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISampleStreaming_ConsumerGrain))]
    internal class OrleansCodeGenSampleStreaming_ConsumerGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISampleStreaming_ConsumerGrain
    {
        protected @OrleansCodeGenSampleStreaming_ConsumerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSampleStreaming_ConsumerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -124728426;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISampleStreaming_ConsumerGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -124728426;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -124728426:
                    switch (@methodId)
                    {
                        case 210218485:
                            return "BecomeConsumer";
                        case 1151053849:
                            return "StopConsuming";
                        case 2130169286:
                            return "GetNumberConsumed";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -124728426 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @BecomeConsumer(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::System.Object>(210218485, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task @StopConsuming()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1151053849, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberConsumed()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(2130169286, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISampleStreaming_ConsumerGrain", -124728426, typeof (global::UnitTests.GrainInterfaces.ISampleStreaming_ConsumerGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSampleStreaming_ConsumerGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -124728426:
                        switch (@methodId)
                        {
                            case 210218485:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_ConsumerGrain)@grain).@BecomeConsumer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 1151053849:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_ConsumerGrain)@grain).@StopConsuming().@Box();
                            case 2130169286:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_ConsumerGrain)@grain).@GetNumberConsumed().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -124728426 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -124728426;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain))]
    internal class OrleansCodeGenSampleStreaming_InlineConsumerGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain
    {
        protected @OrleansCodeGenSampleStreaming_InlineConsumerGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSampleStreaming_InlineConsumerGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -2090853829;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -2090853829 || @interfaceId == -124728426;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -2090853829:
                    switch (@methodId)
                    {
                        case 210218485:
                            return "BecomeConsumer";
                        case 1151053849:
                            return "StopConsuming";
                        case 2130169286:
                            return "GetNumberConsumed";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -2090853829 + ",methodId=" + @methodId);
                    }

                case -124728426:
                    switch (@methodId)
                    {
                        case 210218485:
                            return "BecomeConsumer";
                        case 1151053849:
                            return "StopConsuming";
                        case 2130169286:
                            return "GetNumberConsumed";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -124728426 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @BecomeConsumer(global::System.Guid @streamId, global::System.String @streamNamespace, global::System.String @providerToUse)
        {
            return base.@InvokeMethodAsync<global::System.Object>(210218485, new global::System.Object[]{@streamId, @streamNamespace, @providerToUse});
        }

        public global::System.Threading.Tasks.Task @StopConsuming()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1151053849, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberConsumed()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(2130169286, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain", -2090853829, typeof (global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSampleStreaming_InlineConsumerGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -2090853829:
                        switch (@methodId)
                        {
                            case 210218485:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain)@grain).@BecomeConsumer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 1151053849:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain)@grain).@StopConsuming().@Box();
                            case 2130169286:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain)@grain).@GetNumberConsumed().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -2090853829 + ",methodId=" + @methodId);
                        }

                    case -124728426:
                        switch (@methodId)
                        {
                            case 210218485:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain)@grain).@BecomeConsumer((global::System.Guid)@arguments[0], (global::System.String)@arguments[1], (global::System.String)@arguments[2]).@Box();
                            case 1151053849:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain)@grain).@StopConsuming().@Box();
                            case 2130169286:
                                return ((global::UnitTests.GrainInterfaces.ISampleStreaming_InlineConsumerGrain)@grain).@GetNumberConsumed().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -124728426 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -2090853829;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISimpleGenericGrain<>))]
    internal class OrleansCodeGenSimpleGenericGrainReference<T> : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>
    {
        protected @OrleansCodeGenSimpleGenericGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSimpleGenericGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1882632188;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1882632188 || @interfaceId == 738359988;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 738359988:
                    switch (@methodId)
                    {
                        case -1901180668:
                            return "Set";
                        case -2090495956:
                            return "Transform";
                        case -940922787:
                            return "Get";
                        case 254280809:
                            return "CompareGrainReferences";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 738359988 + ",methodId=" + @methodId);
                    }

                case -1882632188:
                    switch (@methodId)
                    {
                        case -1901180668:
                            return "Set";
                        case -2090495956:
                            return "Transform";
                        case -940922787:
                            return "Get";
                        case 254280809:
                            return "CompareGrainReferences";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1882632188 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @Set(T @t)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1901180668, new global::System.Object[]{@t});
        }

        public global::System.Threading.Tasks.Task @Transform()
        {
            return base.@InvokeMethodAsync<global::System.Object>(-2090495956, null);
        }

        public global::System.Threading.Tasks.Task<T> @Get()
        {
            return base.@InvokeMethodAsync<T>(-940922787, null);
        }

        public global::System.Threading.Tasks.Task @CompareGrainReferences(global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T> @clientRef)
        {
            return base.@InvokeMethodAsync<global::System.Object>(254280809, new global::System.Object[]{@clientRef is global::Orleans.Grain ? @clientRef.@AsReference<global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>>() : @clientRef});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>", -1882632188, typeof (global::UnitTests.GrainInterfaces.ISimpleGenericGrain<>)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSimpleGenericGrainMethodInvoker<T> : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 738359988:
                        switch (@methodId)
                        {
                            case -1901180668:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@grain).@Set((T)@arguments[0]).@Box();
                            case -2090495956:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@grain).@Transform().@Box();
                            case -940922787:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@grain).@Get().@Box();
                            case 254280809:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@grain).@CompareGrainReferences((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 738359988 + ",methodId=" + @methodId);
                        }

                    case -1882632188:
                        switch (@methodId)
                        {
                            case -1901180668:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@grain).@Set((T)@arguments[0]).@Box();
                            case -2090495956:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@grain).@Transform().@Box();
                            case -940922787:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@grain).@Get().@Box();
                            case 254280809:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@grain).@CompareGrainReferences((global::UnitTests.GrainInterfaces.ISimpleGenericGrain<T>)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1882632188 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1882632188;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISomeGrain))]
    internal class OrleansCodeGenSomeGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISomeGrain
    {
        protected @OrleansCodeGenSomeGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSomeGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -914758024;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISomeGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -914758024;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -914758024:
                    switch (@methodId)
                    {
                        case 2103304988:
                            return "Do";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -914758024 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @Do(Outsider @o)
        {
            return base.@InvokeMethodAsync<global::System.Object>(2103304988, new global::System.Object[]{@o});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISomeGrain", -914758024, typeof (global::UnitTests.GrainInterfaces.ISomeGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSomeGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -914758024:
                        switch (@methodId)
                        {
                            case 2103304988:
                                return ((global::UnitTests.GrainInterfaces.ISomeGrain)@grain).@Do((Outsider)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -914758024 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -914758024;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (Outsider)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOutsiderSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            Outsider input = ((Outsider)original);
            Outsider result = new Outsider();
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            Outsider input = (Outsider)untypedInput;
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            Outsider result = new Outsider();
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            return (Outsider)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (Outsider), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOutsiderSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISimpleGrain))]
    internal class OrleansCodeGenSimpleGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISimpleGrain
    {
        protected @OrleansCodeGenSimpleGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSimpleGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1638410893;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISimpleGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1638410893;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1638410893:
                    switch (@methodId)
                    {
                        case 2129932222:
                            return "SetA";
                        case 638886962:
                            return "SetB";
                        case 1017190206:
                            return "IncrementA";
                        case 1039727631:
                            return "GetAxB";
                        case 598136665:
                            return "GetAxB";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1638410893 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetA(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(2129932222, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @SetB(global::System.Int32 @b)
        {
            return base.@InvokeMethodAsync<global::System.Object>(638886962, new global::System.Object[]{@b});
        }

        public global::System.Threading.Tasks.Task @IncrementA()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1017190206, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetAxB()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(1039727631, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetAxB(global::System.Int32 @a, global::System.Int32 @b)
        {
            return base.@InvokeMethodAsync<global::System.Int32>(598136665, new global::System.Object[]{@a, @b});
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetA()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-411561932, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISimpleGrain", 1638410893, typeof (global::UnitTests.GrainInterfaces.ISimpleGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSimpleGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1638410893:
                        switch (@methodId)
                        {
                            case 2129932222:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 638886962:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGrain)@grain).@SetB((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGrain)@grain).@IncrementA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGrain)@grain).@GetAxB().@Box();
                            case 598136665:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGrain)@grain).@GetAxB((global::System.Int32)@arguments[0], (global::System.Int32)@arguments[1]).@Box();
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimpleGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1638410893 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1638410893;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISimpleObserverableGrain))]
    internal class OrleansCodeGenSimpleObserverableGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISimpleObserverableGrain
    {
        protected @OrleansCodeGenSimpleObserverableGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSimpleObserverableGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1750443332;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISimpleObserverableGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1750443332 || @interfaceId == 1638410893;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1750443332:
                    switch (@methodId)
                    {
                        case 1453769850:
                            return "Subscribe";
                        case -1935244785:
                            return "Unsubscribe";
                        case 2129932222:
                            return "SetA";
                        case 638886962:
                            return "SetB";
                        case 1017190206:
                            return "IncrementA";
                        case 1039727631:
                            return "GetAxB";
                        case 598136665:
                            return "GetAxB";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1750443332 + ",methodId=" + @methodId);
                    }

                case 1638410893:
                    switch (@methodId)
                    {
                        case 2129932222:
                            return "SetA";
                        case 638886962:
                            return "SetB";
                        case 1017190206:
                            return "IncrementA";
                        case 1039727631:
                            return "GetAxB";
                        case 598136665:
                            return "GetAxB";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1638410893 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @Subscribe(global::UnitTests.GrainInterfaces.ISimpleGrainObserver @observer)
        {
            global::Orleans.CodeGeneration.GrainFactoryBase.@CheckGrainObserverParamInternal(@observer);
            return base.@InvokeMethodAsync<global::System.Object>(1453769850, new global::System.Object[]{@observer is global::Orleans.Grain ? @observer.@AsReference<global::UnitTests.GrainInterfaces.ISimpleGrainObserver>() : @observer});
        }

        public global::System.Threading.Tasks.Task @Unsubscribe(global::UnitTests.GrainInterfaces.ISimpleGrainObserver @observer)
        {
            global::Orleans.CodeGeneration.GrainFactoryBase.@CheckGrainObserverParamInternal(@observer);
            return base.@InvokeMethodAsync<global::System.Object>(-1935244785, new global::System.Object[]{@observer is global::Orleans.Grain ? @observer.@AsReference<global::UnitTests.GrainInterfaces.ISimpleGrainObserver>() : @observer});
        }

        public global::System.Threading.Tasks.Task @SetA(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(2129932222, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @SetB(global::System.Int32 @b)
        {
            return base.@InvokeMethodAsync<global::System.Object>(638886962, new global::System.Object[]{@b});
        }

        public global::System.Threading.Tasks.Task @IncrementA()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1017190206, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetAxB()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(1039727631, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetAxB(global::System.Int32 @a, global::System.Int32 @b)
        {
            return base.@InvokeMethodAsync<global::System.Int32>(598136665, new global::System.Object[]{@a, @b});
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetA()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-411561932, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISimpleObserverableGrain", -1750443332, typeof (global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSimpleObserverableGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1750443332:
                        switch (@methodId)
                        {
                            case 1453769850:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@Subscribe((global::UnitTests.GrainInterfaces.ISimpleGrainObserver)@arguments[0]).@Box();
                            case -1935244785:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@Unsubscribe((global::UnitTests.GrainInterfaces.ISimpleGrainObserver)@arguments[0]).@Box();
                            case 2129932222:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 638886962:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@SetB((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@IncrementA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@GetAxB().@Box();
                            case 598136665:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@GetAxB((global::System.Int32)@arguments[0], (global::System.Int32)@arguments[1]).@Box();
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1750443332 + ",methodId=" + @methodId);
                        }

                    case 1638410893:
                        switch (@methodId)
                        {
                            case 2129932222:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 638886962:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@SetB((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@IncrementA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@GetAxB().@Box();
                            case 598136665:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@GetAxB((global::System.Int32)@arguments[0], (global::System.Int32)@arguments[1]).@Box();
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1638410893 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1750443332;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISimpleGrainObserver))]
    internal class OrleansCodeGenSimpleGrainObserverReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISimpleGrainObserver
    {
        protected @OrleansCodeGenSimpleGrainObserverReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSimpleGrainObserverReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1394652141;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISimpleGrainObserver";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1394652141;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1394652141:
                    switch (@methodId)
                    {
                        case 938096474:
                            return "StateChanged";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1394652141 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public void @StateChanged(global::System.Int32 @a, global::System.Int32 @b)
        {
            base.@InvokeOneWayMethod(938096474, new global::System.Object[]{@a, @b});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISimpleGrainObserver", -1394652141, typeof (global::UnitTests.GrainInterfaces.ISimpleGrainObserver)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSimpleGrainObserverMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1394652141:
                        switch (@methodId)
                        {
                            case 938096474:
                                ((global::UnitTests.GrainInterfaces.ISimpleGrainObserver)@grain).@StateChanged((global::System.Int32)@arguments[0], (global::System.Int32)@arguments[1]);
                                return global::Orleans.Async.TaskUtility.@Completed();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1394652141 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1394652141;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.IObserverGrain))]
    internal class OrleansCodeGenObserverGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.IObserverGrain
    {
        protected @OrleansCodeGenObserverGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenObserverGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -378267896;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.IObserverGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -378267896;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -378267896:
                    switch (@methodId)
                    {
                        case -1674744194:
                            return "SetTarget";
                        case 1453769850:
                            return "Subscribe";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -378267896 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetTarget(global::UnitTests.GrainInterfaces.ISimpleObserverableGrain @target)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1674744194, new global::System.Object[]{@target is global::Orleans.Grain ? @target.@AsReference<global::UnitTests.GrainInterfaces.ISimpleObserverableGrain>() : @target});
        }

        public global::System.Threading.Tasks.Task @Subscribe(global::UnitTests.GrainInterfaces.ISimpleGrainObserver @observer)
        {
            global::Orleans.CodeGeneration.GrainFactoryBase.@CheckGrainObserverParamInternal(@observer);
            return base.@InvokeMethodAsync<global::System.Object>(1453769850, new global::System.Object[]{@observer is global::Orleans.Grain ? @observer.@AsReference<global::UnitTests.GrainInterfaces.ISimpleGrainObserver>() : @observer});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.IObserverGrain", -378267896, typeof (global::UnitTests.GrainInterfaces.IObserverGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenObserverGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -378267896:
                        switch (@methodId)
                        {
                            case -1674744194:
                                return ((global::UnitTests.GrainInterfaces.IObserverGrain)@grain).@SetTarget((global::UnitTests.GrainInterfaces.ISimpleObserverableGrain)@arguments[0]).@Box();
                            case 1453769850:
                                return ((global::UnitTests.GrainInterfaces.IObserverGrain)@grain).@Subscribe((global::UnitTests.GrainInterfaces.ISimpleGrainObserver)@arguments[0]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -378267896 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -378267896;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ISimplePersistentGrain))]
    internal class OrleansCodeGenSimplePersistentGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ISimplePersistentGrain
    {
        protected @OrleansCodeGenSimplePersistentGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenSimplePersistentGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1227585576;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ISimplePersistentGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1227585576 || @interfaceId == 1638410893;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1227585576:
                    switch (@methodId)
                    {
                        case 1671394104:
                            return "SetA";
                        case 2090373029:
                            return "GetVersion";
                        case 2129932222:
                            return "SetA";
                        case 638886962:
                            return "SetB";
                        case 1017190206:
                            return "IncrementA";
                        case 1039727631:
                            return "GetAxB";
                        case 598136665:
                            return "GetAxB";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1227585576 + ",methodId=" + @methodId);
                    }

                case 1638410893:
                    switch (@methodId)
                    {
                        case 2129932222:
                            return "SetA";
                        case 638886962:
                            return "SetB";
                        case 1017190206:
                            return "IncrementA";
                        case 1039727631:
                            return "GetAxB";
                        case 598136665:
                            return "GetAxB";
                        case -411561932:
                            return "GetA";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1638410893 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @SetA(global::System.Int32 @a, global::System.Boolean @deactivate)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1671394104, new global::System.Object[]{@a, @deactivate});
        }

        public global::System.Threading.Tasks.Task<global::System.Guid> @GetVersion()
        {
            return base.@InvokeMethodAsync<global::System.Guid>(2090373029, null);
        }

        public global::System.Threading.Tasks.Task @SetA(global::System.Int32 @a)
        {
            return base.@InvokeMethodAsync<global::System.Object>(2129932222, new global::System.Object[]{@a});
        }

        public global::System.Threading.Tasks.Task @SetB(global::System.Int32 @b)
        {
            return base.@InvokeMethodAsync<global::System.Object>(638886962, new global::System.Object[]{@b});
        }

        public global::System.Threading.Tasks.Task @IncrementA()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1017190206, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetAxB()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(1039727631, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetAxB(global::System.Int32 @a, global::System.Int32 @b)
        {
            return base.@InvokeMethodAsync<global::System.Int32>(598136665, new global::System.Object[]{@a, @b});
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetA()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(-411561932, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ISimplePersistentGrain", 1227585576, typeof (global::UnitTests.GrainInterfaces.ISimplePersistentGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenSimplePersistentGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case 1227585576:
                        switch (@methodId)
                        {
                            case 1671394104:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@SetA((global::System.Int32)@arguments[0], (global::System.Boolean)@arguments[1]).@Box();
                            case 2090373029:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@GetVersion().@Box();
                            case 2129932222:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 638886962:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@SetB((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@IncrementA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@GetAxB().@Box();
                            case 598136665:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@GetAxB((global::System.Int32)@arguments[0], (global::System.Int32)@arguments[1]).@Box();
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1227585576 + ",methodId=" + @methodId);
                        }

                    case 1638410893:
                        switch (@methodId)
                        {
                            case 2129932222:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@SetA((global::System.Int32)@arguments[0]).@Box();
                            case 638886962:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@SetB((global::System.Int32)@arguments[0]).@Box();
                            case 1017190206:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@IncrementA().@Box();
                            case 1039727631:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@GetAxB().@Box();
                            case 598136665:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@GetAxB((global::System.Int32)@arguments[0], (global::System.Int32)@arguments[1]).@Box();
                            case -411561932:
                                return ((global::UnitTests.GrainInterfaces.ISimplePersistentGrain)@grain).@GetA().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1638410893 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1227585576;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::UnitTests.GrainInterfaces.ILivenessTestGrain))]
    internal class OrleansCodeGenLivenessTestGrainReference : global::Orleans.Runtime.GrainReference, global::UnitTests.GrainInterfaces.ILivenessTestGrain
    {
        protected @OrleansCodeGenLivenessTestGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenLivenessTestGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1840629153;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::UnitTests.GrainInterfaces.ILivenessTestGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1840629153;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1840629153:
                    switch (@methodId)
                    {
                        case 815427771:
                            return "GetLabel";
                        case 137111425:
                            return "SetLabel";
                        case 327261474:
                            return "GetRuntimeInstanceId";
                        case 1156956714:
                            return "GetUniqueId";
                        case 2018329561:
                            return "GetGrainReference";
                        case -1685414735:
                            return "StartTimer";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1840629153 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetLabel()
        {
            return base.@InvokeMethodAsync<global::System.String>(815427771, null);
        }

        public global::System.Threading.Tasks.Task @SetLabel(global::System.String @label)
        {
            return base.@InvokeMethodAsync<global::System.Object>(137111425, new global::System.Object[]{@label});
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetRuntimeInstanceId()
        {
            return base.@InvokeMethodAsync<global::System.String>(327261474, null);
        }

        public global::System.Threading.Tasks.Task<global::System.String> @GetUniqueId()
        {
            return base.@InvokeMethodAsync<global::System.String>(1156956714, null);
        }

        public global::System.Threading.Tasks.Task<global::UnitTests.GrainInterfaces.ILivenessTestGrain> @GetGrainReference()
        {
            return base.@InvokeMethodAsync<global::UnitTests.GrainInterfaces.ILivenessTestGrain>(2018329561, null);
        }

        public global::System.Threading.Tasks.Task @StartTimer()
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1685414735, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.11.8"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::UnitTests.GrainInterfaces.ILivenessTestGrain", -1840629153, typeof (global::UnitTests.GrainInterfaces.ILivenessTestGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenLivenessTestGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::System.Int32 @interfaceId, global::System.Int32 @methodId, global::System.Object[] @arguments)
        {
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (@interfaceId)
                {
                    case -1840629153:
                        switch (@methodId)
                        {
                            case 815427771:
                                return ((global::UnitTests.GrainInterfaces.ILivenessTestGrain)@grain).@GetLabel().@Box();
                            case 137111425:
                                return ((global::UnitTests.GrainInterfaces.ILivenessTestGrain)@grain).@SetLabel((global::System.String)@arguments[0]).@Box();
                            case 327261474:
                                return ((global::UnitTests.GrainInterfaces.ILivenessTestGrain)@grain).@GetRuntimeInstanceId().@Box();
                            case 1156956714:
                                return ((global::UnitTests.GrainInterfaces.ILivenessTestGrain)@grain).@GetUniqueId().@Box();
                            case 2018329561:
                                return ((global::UnitTests.GrainInterfaces.ILivenessTestGrain)@grain).@GetGrainReference().@Box();
                            case -1685414735:
                                return ((global::UnitTests.GrainInterfaces.ILivenessTestGrain)@grain).@StartTimer().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -1840629153 + ",methodId=" + @methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1840629153;
            }
        }
    }
}
#pragma warning restore 162
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 649
#pragma warning restore 693
#pragma warning restore 1591
#pragma warning restore 1998
#endif
