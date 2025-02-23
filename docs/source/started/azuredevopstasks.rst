Getting started with Esquio Azure DevOps tasks
============================================

In this article, we are going to see how to configure Esquio Azure DevOps tasks for your pipelines. 

> In `samples/WebApp <https://github.com/Xabaril/Esquio/tree/master/samples/WebApp>`_ you'll find a complete Esquio example in ASP.NET Core.

Setup
^^^^^
The first step is install the `Esquio Azure DevOps Task from Visual Studio Marketplace <https://marketplace.visualstudio.com/items?itemName=xabaril.esquio*extensions>`_ . There isn't anything special needed, just install it as a normal Azure DevOps extension.

Once installed, you will have new elements in your Azure DevOps:

    * Esquio Service Connection
    * Esquio rollout task
    * Esquio rollback task
    * Esquio set parameter value task

Prerequisites
^^^^^^^^^^^^^
To be able to use Esquio tasks, we will need to setup a Esquio `Service Connection  <https://docs.microsoft.com/en-us/azure/devops/pipelines/library/service-endpoints?view=azure-devops&tabs=yaml>`_ and to configure it you need to create an Esquio API key, to use it with the Esquio Service Connection.

Setup Esquio Service Connection
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
`Service Connection  <https://docs.microsoft.com/en-us/azure/devops/pipelines/library/service-endpoints?view=azure-devops&tabs=yaml>`_ are setup per project, so open your Azure DevOps settings page, and go to Service Connections, click on *New service Connection* and select *Esquio API Connection*

.. image:: ../images/service-connection.png

This will bring the *Esquio API Connection* configuration screen, here you need to setup three parameters:

    * **Connection name:** To use it in the Azure DevOps tasks.
    * **Esquio API Url:** The complete url in which yoy have your Esquio API.
    * **API token:** The Esquio API key you have setup.

.. image:: ../images/service-connection-form.png

Once filled all the information, make sure it is correct, clicking on *Verify connection* and make sure it says *Connection: Verified*

.. image:: ../images/service-connection-verified.png

Now you have setup the Esquio Connection we will need to use for the tasks.

Esquio rollout task
^^^^^^^^^^^^^^^^^^^
This task allow us to set a :doc:`OnToggle <../toggles/esquio>` for a feature.

If you are creating your Azure Pipelines with *YAML* it is better to use the *YAML assistant* as it will allow you to use the datasources for the picklists.

.. image:: ../images/pipeline-assistant.png

To setup the rollout task, look for **Rollout feature with Esquio** task:

.. image:: ../images/rollout-blank.png

We will configure three parameters:

    * **Esquio Service Endpoint:** Select the previously created *Esquio Service Connection*.
    * **Esquio Product:** From the list of products configured in Esquio, select the one with the feature you want to setup de *OnToggle*.
    * **Esquio feature:** Select, from the list of features, select the one to setup the *OnToggle*.

The final YAML should be (with different ids) like this::

        - task: esquio-rollout-feature@1
          inputs:
            EsquioService: 'Esquio'
            productId: '1'
            flagId: '1'

If you are using the classic pipelines (the visual ones), the setup is exactly the same.