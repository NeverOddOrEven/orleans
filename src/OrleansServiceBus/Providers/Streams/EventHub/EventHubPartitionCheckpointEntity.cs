﻿
using System;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage.Table;
using Orleans.AzureUtils;

namespace Orleans.ServiceBus.Providers
{
    internal class EventHubPartitionCheckpointEntity : TableEntity
    {
        public string Offset { get; set; }

        public EventHubPartitionCheckpointEntity()
        {
            Offset = EventHubConsumerGroup.StartOfStream;
        }

        public static EventHubPartitionCheckpointEntity Create(string streamProviderName, string checkpointNamespace, string partition)
        {
            return new EventHubPartitionCheckpointEntity
            {
                PartitionKey = MakePartitionKey(streamProviderName, checkpointNamespace),
                RowKey = MakeRowKey(partition)
            };
        }

        public static string MakePartitionKey(string streamProviderName, string checkpointNamespace)
        {
            string key = String.Format("EHCheckpoints_{0}_{1}", streamProviderName, checkpointNamespace);
            return AzureStorageUtils.SanitizeTableProperty(key);
        }

        public static string MakeRowKey(string partition)
        {
            string key = String.Format("partition_{0}", partition);
            return AzureStorageUtils.SanitizeTableProperty(key);
        }
    }
}
