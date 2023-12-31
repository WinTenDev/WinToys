﻿using System;
using System.Windows;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Mvvm.Contracts;

namespace WinToys.Services;

/// <summary>
/// Service that provides pages for navigation.
/// </summary>
public class PageService : IPageService
{
    /// <summary>
    /// Service which provides the instances of pages.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    private readonly ILogger<PageService> _logger;

    /// <summary>
    /// Creates new instance and attaches the <see cref="IServiceProvider"/>.
    /// </summary>
    public PageService(IServiceProvider serviceProvider, ILogger<PageService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <inheritdoc />
    public T? GetPage<T>() where T : class
    {
        if (!typeof(FrameworkElement).IsAssignableFrom(typeof(T)))
            throw new InvalidOperationException("The page should be a WPF control.");

        return (T?)_serviceProvider.GetService(typeof(T));
    }

    /// <inheritdoc />
    public FrameworkElement? GetPage(Type pageType)
    {
        _logger.LogDebug("Navigating to page: {Page}", pageType.FullName);

        if (!typeof(FrameworkElement).IsAssignableFrom(pageType))
            throw new InvalidOperationException("The page should be a WPF control.");

        return _serviceProvider.GetService(pageType) as FrameworkElement;
    }
}