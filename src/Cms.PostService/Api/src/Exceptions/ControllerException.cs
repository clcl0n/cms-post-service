using System;

namespace Cms.PostService.Api.Exceptions;

public class ControllerException(string? message) : Exception(message);
