namespace Sonos.Base;
using Sonos.Base.Services;
public partial class SonosDevice {
  private AlarmClockService? _alarmclock;
  
  public AlarmClockService AlarmClockService {
    get {
      if(_alarmclock is null) {
        _alarmclock = new AlarmClockService(deviceUri, httpClient);
      }
      return _alarmclock;
    }
  }
  private AudioInService? _audioin;
  
  public AudioInService AudioInService {
    get {
      if(_audioin is null) {
        _audioin = new AudioInService(deviceUri, httpClient);
      }
      return _audioin;
    }
  }
  private AVTransportService? _avtransport;
  
  public AVTransportService AVTransportService {
    get {
      if(_avtransport is null) {
        _avtransport = new AVTransportService(deviceUri, httpClient);
      }
      return _avtransport;
    }
  }
  private ConnectionManagerService? _connectionmanager;
  
  public ConnectionManagerService ConnectionManagerService {
    get {
      if(_connectionmanager is null) {
        _connectionmanager = new ConnectionManagerService(deviceUri, httpClient);
      }
      return _connectionmanager;
    }
  }
  private ContentDirectoryService? _contentdirectory;
  
  public ContentDirectoryService ContentDirectoryService {
    get {
      if(_contentdirectory is null) {
        _contentdirectory = new ContentDirectoryService(deviceUri, httpClient);
      }
      return _contentdirectory;
    }
  }
  private DevicePropertiesService? _deviceproperties;
  
  public DevicePropertiesService DevicePropertiesService {
    get {
      if(_deviceproperties is null) {
        _deviceproperties = new DevicePropertiesService(deviceUri, httpClient);
      }
      return _deviceproperties;
    }
  }
  private GroupManagementService? _groupmanagement;
  
  public GroupManagementService GroupManagementService {
    get {
      if(_groupmanagement is null) {
        _groupmanagement = new GroupManagementService(deviceUri, httpClient);
      }
      return _groupmanagement;
    }
  }
  private GroupRenderingControlService? _grouprenderingcontrol;
  
  public GroupRenderingControlService GroupRenderingControlService {
    get {
      if(_grouprenderingcontrol is null) {
        _grouprenderingcontrol = new GroupRenderingControlService(deviceUri, httpClient);
      }
      return _grouprenderingcontrol;
    }
  }
  private HTControlService? _htcontrol;
  
  public HTControlService HTControlService {
    get {
      if(_htcontrol is null) {
        _htcontrol = new HTControlService(deviceUri, httpClient);
      }
      return _htcontrol;
    }
  }
  private MusicServicesService? _musicservices;
  
  public MusicServicesService MusicServicesService {
    get {
      if(_musicservices is null) {
        _musicservices = new MusicServicesService(deviceUri, httpClient);
      }
      return _musicservices;
    }
  }
  private QPlayService? _qplay;
  
  public QPlayService QPlayService {
    get {
      if(_qplay is null) {
        _qplay = new QPlayService(deviceUri, httpClient);
      }
      return _qplay;
    }
  }
  private QueueService? _queue;
  
  public QueueService QueueService {
    get {
      if(_queue is null) {
        _queue = new QueueService(deviceUri, httpClient);
      }
      return _queue;
    }
  }
  private RenderingControlService? _renderingcontrol;
  
  public RenderingControlService RenderingControlService {
    get {
      if(_renderingcontrol is null) {
        _renderingcontrol = new RenderingControlService(deviceUri, httpClient);
      }
      return _renderingcontrol;
    }
  }
  private SystemPropertiesService? _systemproperties;
  
  public SystemPropertiesService SystemPropertiesService {
    get {
      if(_systemproperties is null) {
        _systemproperties = new SystemPropertiesService(deviceUri, httpClient);
      }
      return _systemproperties;
    }
  }
  private VirtualLineInService? _virtuallinein;
  
  public VirtualLineInService VirtualLineInService {
    get {
      if(_virtuallinein is null) {
        _virtuallinein = new VirtualLineInService(deviceUri, httpClient);
      }
      return _virtuallinein;
    }
  }
  private ZoneGroupTopologyService? _zonegrouptopology;
  
  public ZoneGroupTopologyService ZoneGroupTopologyService {
    get {
      if(_zonegrouptopology is null) {
        _zonegrouptopology = new ZoneGroupTopologyService(deviceUri, httpClient);
      }
      return _zonegrouptopology;
    }
  }
}


