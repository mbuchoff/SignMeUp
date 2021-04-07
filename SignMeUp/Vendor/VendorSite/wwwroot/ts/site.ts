import Calendar, { IEventScheduleObject, ISchedule } from 'tui-calendar';
import "tui-calendar/dist/tui-calendar.css";
import "bootstrap/dist/css/bootstrap.min.css"

// If you use the default popups, use this.
import 'tui-date-picker/dist/tui-date-picker.css';
import 'tui-time-picker/dist/tui-time-picker.css';

var cal = new Calendar('#calendar', {
    useCreationPopup: true,
    defaultView: 'week', // set 'week' or 'day'
    taskView: true,  // e.g. true, false, or ['task', 'milestone'])
    scheduleView: ['time']  // e.g. true, false, or ['allday', 'time'])
});

cal.on('beforeCreateSchedule', function (event: ISchedule) {
    event.category = 'time';
    cal.createSchedules([event]);
});

let lastClickSchedule: ISchedule | null = null;
cal.on('clickSchedule', function (event: IEventScheduleObject) {
    var schedule = event.schedule;

    if (lastClickSchedule) {
        if (!lastClickSchedule.id) {
            alert('sanity check fail, lastClickSchedule.id is null');
        } else if (!lastClickSchedule.calendarId) {
            alert('sanity check fail, lastClickSchedule.calendarId is null');
        } else {
            cal.updateSchedule(lastClickSchedule.id, lastClickSchedule.calendarId, {
                isFocused: false
            });
        }
    }

    if (schedule.id == null) {
        alert('sanity check fail, schedule.id is null');
    } else if (schedule.calendarId == null) {
        alert('sanity check fail, schedule.calendarId is null');
    } else {
        cal.updateSchedule(schedule.id, schedule.calendarId, {
            isFocused: true
        });
    }

    lastClickSchedule = schedule;
    alert(`lastClickSchedule.id is ${lastClickSchedule.id}`);
    // open detail view
});